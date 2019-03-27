using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.DAC;
using PX.Objects.TX;
using PX.Objects.CS;
using PX.Objects.AP;
using PX.OAuthClient.DAC;
using PX.HMRC.DAC;
using PX.HMRC.Model;

namespace PX.HMRC
{
	[PX.Objects.GL.TableAndChartDashboardType]
	public class VATMaint : PXGraph<VATMaint>
	{
		public PXFilter<VATPeriodFilter> Period_Header;
		public PXCancel<VATPeriodFilter> Cancel;

		#region VATRows
		public PXSelect<VATRow> VATRows;

		protected virtual IEnumerable vATRows()
		{
            PXCache<VATRow> cache = this.Caches<VATRow>();
            foreach (var i in this.Caches<VATRow>().Cached)
				if (cache.GetStatus(i) != PXEntryStatus.Deleted && cache.GetStatus(i) != PXEntryStatus.InsertedDeleted)
					yield return i;
		}
		#endregion

		#region Obligations
		public PXSelect<Obligation> Obligations;
		public virtual IEnumerable obligations()
		{
			PXCache<Obligation> cache = this.Caches<Obligation>();

            if ((this.Caches<Obligation>().Cached as IEnumerable<Obligation>).Count() == 0)
            {
                if (Period_Header.Current?.EndDate != null)
                {
                    GetVATObligationsForYearProc(this, Period_Header.Current.EndDate.Value);
                }
            }

            foreach (Obligation o in cache.Cached)
                if (cache.GetStatus(o) != PXEntryStatus.Deleted && cache.GetStatus(o) != PXEntryStatus.InsertedDeleted)
                    yield return o;
        }
        #endregion

        #region BQL Selectors 
        public PXSelect<TaxPeriod,
					Where<TaxPeriod.organizationID, Equal<Current<VATPeriodFilter.organizationID>>,
								And<TaxPeriod.vendorID, Equal<Current<VATPeriodFilter.vendorID>>,
								And<TaxPeriod.taxPeriodID, Equal<Current<VATPeriodFilter.taxPeriodID>>>>>> Period;

		public PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Current<VATPeriodFilter.vendorID>>>> Vendor;
		#endregion

		#region internal
		public override bool IsDirty { get { return false; } }
		#endregion

		#region Period_Details
		public PXSelectJoin<TaxReportLine,
					LeftJoin<TaxHistoryReleased,
						On<TaxHistoryReleased.vendorID, Equal<TaxReportLine.vendorID>,
						And<TaxHistoryReleased.lineNbr, Equal<TaxReportLine.lineNbr>>>>,
					Where<False, Equal<True>>,
					OrderBy<
						Asc<TaxReportLine.sortOrder,
						Asc<TaxReportLine.taxZoneID>>>> Period_Details;

		public PXSelectJoinGroupBy<TaxReportLine,
			LeftJoin<TaxHistoryReleased,
				On<TaxHistoryReleased.vendorID, Equal<TaxReportLine.vendorID>,
				And<TaxHistoryReleased.lineNbr, Equal<TaxReportLine.lineNbr>,
				And<TaxHistoryReleased.taxPeriodID, Equal<Current<VATPeriodFilter.taxPeriodID>>,
				And<TaxHistoryReleased.revisionID, Equal<Current<VATPeriodFilter.revisionId>>>>>>>,
			Where<TaxReportLine.vendorID, Equal<Current<VATPeriodFilter.vendorID>>,
				And<TaxReportLine.tempLine, Equal<False>,
				And2<
					Where<TaxReportLine.tempLineNbr, IsNull, Or<TaxHistoryReleased.vendorID, IsNotNull>>,
					And<Where<TaxReportLine.hideReportLine, IsNull, Or<TaxReportLine.hideReportLine, Equal<False>>>>>
				>>,
			Aggregate<
				GroupBy<TaxReportLine.lineNbr,
					Sum<TaxHistoryReleased.filedAmt,
					Sum<TaxHistoryReleased.reportFiledAmt>>>>> Period_Details_Expanded;

		protected virtual IEnumerable period_Details()
		{
			VATPeriodFilter filter = Period_Header.Current;

			using (new PXReadBranchRestrictedScope(filter.OrganizationID.SingleToArray(), filter.BranchID.SingleToArrayOrNull()))
			{
                return Period_Details_Expanded.Select();
			}
		}

		#endregion

		#region Private variables
		private HMRCOAuthApplication _oAuthApplication;
		private OAuthToken _oAuthToken;
		private const string _applicationName= "HMRC";
		private string vrn = "";
		private string urlSite = "https://test-api.service.hmrc.gov.uk";

        private VATApi VATProvider = null;
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public VATMaint()
		{
			_oAuthApplication = PXSelectReadonly<HMRCOAuthApplication,
							Where<HMRCOAuthApplication.applicationName, Equal<Required<HMRCOAuthApplication.applicationName>>>>
			.SelectSingleBound(this, null, _applicationName);
			if (_oAuthApplication != null)
			{
                if (!String.IsNullOrEmpty(_oAuthApplication.UsrServerUrl))
                {
                    urlSite = _oAuthApplication.UsrServerUrl;
                }

                _oAuthToken = PXSelectReadonly<OAuthToken, Where<OAuthToken.applicationID, Equal<Required<OAuthToken.applicationID>>>>
					.SelectSingleBound(this, null, _oAuthApplication.ApplicationID);
			}
            VATProvider = new VATApi(urlSite, _oAuthApplication, _oAuthToken, vrn, UpdateOAuthToken);
            SetVRN(Period_Header.Current);

            signInHMRC.SetEnabled(_oAuthToken == null);
            sendVATreturn.SetEnabled(false);
			checkVATReturn.SetEnabled(false);
            PXUIFieldAttribute.SetEnabled<VATPeriodFilter.periodKey>(Period_Header.Cache, null, _oAuthToken != null);
            PXUIFieldAttribute.SetVisibility<TaxReportLine.lineNbr>(Period_Details.Cache, null, PXUIVisibility.Invisible);
            PXUIFieldAttribute.SetVisibility<TaxReportLine.lineNbr>(Period_Details.Cache, null, PXUIVisibility.Invisible);
        }

        #region VATPeriodFilter Handhers

        [PXDefault]
        [PXUIField(DisplayName = "Tax Period", Visibility = PXUIVisibility.Visible)]
        [FinPeriodID]
        [PXSelector(
            typeof(Search<TaxPeriod.taxPeriodID,
                Where<TaxPeriod.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>,
                    And<TaxPeriod.organizationID, Equal<Current<TaxPeriodFilter.organizationID>>, 
                    And<TaxPeriod.status,Equal<TaxPeriodStatus.closed>>>>>),
            typeof(TaxPeriod.taxPeriodID), typeof(TaxPeriod.startDateUI), typeof(TaxPeriod.endDateUI), typeof(TaxPeriod.status),
            SelectorMode = PXSelectorMode.NoAutocomplete,
            DirtyRead = true)]
        protected void VATPeriodFilter_TaxPeriodID_CacheAttached(PXCache sender) { }

        protected void baseVATPeriodFilterRowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			VATPeriodFilter filter = e.Row as VATPeriodFilter;

			if (filter?.OrganizationID == null)
				return;

			Organization organization = OrganizationMaint.FindOrganizationByID(this, filter.OrganizationID);

			if (organization.FileTaxesByBranches == true && filter.BranchID == null
				|| filter.VendorID == null || filter.TaxPeriodID == null)
				return;

			TaxPeriod taxPeriod = Period.Select();

			int? maxRevision = ReportTaxProcess.CurrentRevisionId(sender.Graph, filter.OrganizationID, filter.BranchID, filter.VendorID, filter.TaxPeriodID);
			filter.StartDate = taxPeriod?.StartDateUI;
			filter.EndDate = taxPeriod?.EndDate != null	? (DateTime?)(((DateTime)taxPeriod.EndDate).AddDays(-1)) : null;

			PXUIFieldAttribute.SetEnabled<VATPeriodFilter.revisionId>(sender, null, maxRevision > 1);
			PXUIFieldAttribute.SetEnabled<VATPeriodFilter.taxPeriodID>(sender, null, true);
		}

		protected void VATPeriodFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			baseVATPeriodFilterRowSelected(sender, e);
			bool hasPeriodKey = (((VATPeriodFilter)(e.Row))?.PeriodKey != null);
			this.sendVATreturn.SetEnabled(hasPeriodKey);
			this.checkVATReturn.SetEnabled(hasPeriodKey);
		}

        protected virtual void baseTaxPeriodFilterRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			VATPeriodFilter filter = (VATPeriodFilter)e.Row;
			if (filter == null) return;

			if (!sender.ObjectsEqual<VATPeriodFilter.organizationID, VATPeriodFilter.branchID>(e.Row, e.OldRow))
			{
				List<PXView> views = this.Views.Select(view => view.Value).ToList();
				foreach (var view in views) view.Clear();
			}

			if (!sender.ObjectsEqual<VATPeriodFilter.organizationID>(e.Row, e.OldRow)
				|| !sender.ObjectsEqual<VATPeriodFilter.branchID>(e.Row, e.OldRow)
				|| !sender.ObjectsEqual<VATPeriodFilter.vendorID>(e.Row, e.OldRow))
			{
				if (filter.OrganizationID != null && filter.VendorID != null)
				{
					PX.Objects.TX.TaxPeriod taxper = TaxYearMaint.FindPreparedPeriod(this, filter.OrganizationID, filter.VendorID);

					if (taxper != null)
					{
						filter.TaxPeriodID = taxper.TaxPeriodID;
					}
					else
					{
						taxper = TaxYearMaint.FindLastClosedPeriod(this, filter.OrganizationID, filter.VendorID);
						filter.TaxPeriodID = taxper != null ? taxper.TaxPeriodID : null;
					}
				}
				else
				{
					filter.TaxPeriodID = null;
				}
			}

			Organization organization = OrganizationMaint.FindOrganizationByID(this, filter.OrganizationID);

			if (!sender.ObjectsEqual<VATPeriodFilter.organizationID>(e.Row, e.OldRow)
				|| !sender.ObjectsEqual<VATPeriodFilter.branchID>(e.Row, e.OldRow)
				|| !sender.ObjectsEqual<VATPeriodFilter.vendorID>(e.Row, e.OldRow)
				|| !sender.ObjectsEqual<VATPeriodFilter.taxPeriodID>(e.Row, e.OldRow)
                || filter.RevisionId==null)
			{
				if (filter.OrganizationID != null
					&& (filter.BranchID != null && organization.FileTaxesByBranches == true || organization.FileTaxesByBranches != true)
					&& filter.VendorID != null && filter.TaxPeriodID != null)
				{
					filter.RevisionId = ReportTaxProcess.CurrentRevisionId(this, filter.OrganizationID, filter.BranchID, filter.VendorID, filter.TaxPeriodID);
				}
				else
				{
					filter.RevisionId = null;
				}
			}
		}

		protected void VATPeriodFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			baseTaxPeriodFilterRowUpdated(sender, e);
			VATPeriodFilter filter = (VATPeriodFilter)e.Row;

            if (!sender.ObjectsEqual<VATPeriodFilter.organizationID>(e.Row, e.OldRow)
                || !sender.ObjectsEqual<VATPeriodFilter.branchID>(e.Row, e.OldRow))
            {
                SetVRN(filter);
            }
            if (!sender.ObjectsEqual<VATPeriodFilter.organizationID>(e.Row, e.OldRow)
                || !sender.ObjectsEqual<VATPeriodFilter.branchID>(e.Row, e.OldRow)
                || !sender.ObjectsEqual<VATPeriodFilter.vendorID>(e.Row, e.OldRow)
                || !sender.ObjectsEqual<VATPeriodFilter.taxPeriodID>(e.Row, e.OldRow)
             )
            {
                filter.Start = null;
                filter.End = null;
                filter.PeriodKey = null;
                filter.Due = null;
                filter.Status = null;
                filter.Received = null;
            }
            if (!sender.ObjectsEqual<VATPeriodFilter.periodKey>(e.Row, e.OldRow))
            {
                Obligation o = (Obligation) this.Obligations.Cache.Locate(new Obligation() { PeriodKey = filter.PeriodKey });
                if (o != null)
                {
                    filter.Start = o.Start;
                    filter.End = o.End;
                    filter.Status = o.Status;
                    filter.Due = o.Due;
                    filter.Received = o.Received;
                }
            }
            if (!sender.ObjectsEqual<VATPeriodFilter.endDate>(e.Row, e.OldRow))
            {
                GetVATObligationsForYearProc(this, filter.EndDate??DateTime.UtcNow);
            }
        }
        #endregion

		#region Actions
		#region SignInHMRC
		public PXAction<VATPeriodFilter> signInHMRC;
		[PXUIField(DisplayName = Messages.SignInHMRC, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable SignInHMRC(PXAdapter adapter)
		{
			VATPeriodFilter filter = Period_Header.Current;
			//PXLongOperation.StartOperation(this, () => VATMaint.SignInHMRCProc(this, filter));
			VATMaint.SignInHMRCProc(this, filter);
			return adapter.Get();
		}
        #endregion

        #region checkVATReturn
        public PXAction<VATPeriodFilter> checkVATReturn;
		[PXUIField(DisplayName = Messages.RetrieveVATreturn, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable CheckVATReturn(PXAdapter adapter)
		{
			VATPeriodFilter tp = Period_Header.Current;
			//PXLongOperation.StartOperation(this, () => HMRCReportTax.CheckVATReturnProc(this, tp));
			VATMaint.CheckVATReturnProc(this, tp.PeriodKey);
			return adapter.Get();
		}
        #endregion

        #region SendVATReturn
        public PXAction<VATPeriodFilter> sendVATreturn;
		[PXUIField(DisplayName = Messages.SubmitVATReturn, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
		[PXProcessButton]
		public virtual IEnumerable SendVATReturn(PXAdapter adapter)
		{
			VATPeriodFilter tp = Period_Header.Current;
            if (tp.RevisionId == null) return adapter.Get();

            WebDialogResult dialogResult = Period_Header.Ask(Messages.VatReturnWillBeSentToHMRC, MessageButtons.YesNoCancel);
            Period_Header.ClearDialog();
            if (dialogResult == WebDialogResult.Cancel) return adapter.Get();

            VATMaint.SendVATReturnProc(this, tp, dialogResult== WebDialogResult.Yes);

            return adapter.Get();
		}
        #endregion

        #region viewTaxDocument
        public PXAction<VATPeriodFilter> viewTaxDocument;
        [PXUIField(DisplayName = PX.Objects.TX.Messages.ViewDocuments, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ViewTaxDocument(PXAdapter adapter)
        {
            if (this.Period_Details.Current != null)
            {
                ReportTaxDetail graph = PXGraph.CreateInstance<ReportTaxDetail>();
                TaxHistoryMaster filter = PXCache<TaxHistoryMaster>.CreateCopy(graph.History_Header.Current);
                filter.OrganizationID = Period_Header.Current.OrganizationID;
                filter.BranchID = Period_Header.Current.BranchID;
                filter.VendorID = Period_Header.Current.VendorID;
                filter.TaxPeriodID = Period_Header.Current.TaxPeriodID;
                filter.LineNbr = Period_Details.Current.LineNbr;
                graph.History_Header.Update(filter);
                throw new PXRedirectRequiredException(graph, PX.Objects.TX.Messages.ViewDocuments);
            }
            return Period_Header.Select();
        }
        #endregion

        #endregion

        public static void SignInHMRCProc(VATMaint graph, VATPeriodFilter filter)
		{
			graph.VATProvider.SignIn();
			throw new PXRefreshException();
		}

		public static void GetVATObligationsForYearProc(VATMaint graph, DateTime to) {
            GetVATObligationsProc(graph, to.AddYears(-1), to);
		}

		public static void GetVATObligationsProc(VATMaint graph, DateTime from, DateTime to, string status=null)
		{
			obligationsRequest req = new obligationsRequest() {	from = from, to = to, status = status };
			obligationResponse obligationResponse=null;
			try
			{
                obligationResponse = graph.VATProvider.Obligations(req);
            }
            catch (Exceptions.VATAPIInvalidToken eToken)
			{
				PXTrace.WriteError(eToken);
                graph.signInHMRC.SetEnabled(true);
                throw new PXException(Messages.PleaseAuthorize);
            }
			catch (Exceptions.VATAPIException eApi)
			{
				PXTrace.WriteError(eApi);
                if (eApi.Data.Contains("json"))
                {
                    PXTrace.WriteError(eApi.Data["json"].ToString());
                }
                if (eApi.Code != error.MATCHING_RESOURCE_NOT_FOUND) {
                    throw eApi;
                }
			}			
			catch (Exception e)
			{
				PXTrace.WriteError(e);
				throw e;
			}

			graph.Obligations.Cache.Clear();
            if(obligationResponse!=null)
            foreach (var o in obligationResponse.obligations)
			{
				graph.Obligations.Insert(new Obligation()
				{
					Start = o.start,
					End = o.end,
					Due = o.due,
					Status = o.status,
					PeriodKey = o.periodKey,
					Received = o.received
				});
			}
			return;
		}

		public static void CheckVATReturnProc(VATMaint graph, string periodKey)
		{
			Model.VATreturn vATreturn;
			try
			{
				vATreturn = graph.VATProvider.Returns(periodKey);
			}
			catch (Exceptions.VATAPIInvalidToken eToken)
			{
				PXTrace.WriteError(eToken);
                graph.signInHMRC.SetEnabled(true);
                throw new PXException(Messages.PleaseAuthorize);
			}
			catch (Exceptions.VATAPIException eApi)
			{
                if (eApi.Code == error.NOT_FOUND)
                {
                    graph.VATRows.Cache.Clear();
                }
				PXTrace.WriteError(eApi);
                if (eApi.Data.Contains("json"))
                {
                    PXTrace.WriteError(eApi.Data["json"].ToString());
                }
                throw eApi;
			}
			catch (Exception e)
			{
				PXTrace.WriteError(e);
				throw e;
			}
			graph.VATRows.Cache.Clear();
			graph.VATRows.Insert(new VATRow() { TaxBoxNbr = "1", TaxBoxCode = "vatDueSales", Descr = "VAT due on sales and other outputs", Amt = vATreturn.vatDueSales });
			graph.VATRows.Insert(new VATRow() { TaxBoxNbr = "2", TaxBoxCode = "vatDueAcquisitions", Descr = "VAT due on acquisitions from other EC Member States.", Amt = vATreturn.vatDueAcquisitions });
			graph.VATRows.Insert(new VATRow() { TaxBoxNbr = "3", TaxBoxCode = "totalVatDue", Descr = "Total VAT due", Amt = vATreturn.totalVatDue });
			graph.VATRows.Insert(new VATRow() { TaxBoxNbr = "4", TaxBoxCode = "vatReclaimedCurrPeriod", Descr = "VAT reclaimed on purchases and other inputs", Amt = vATreturn.vatReclaimedCurrPeriod });
			graph.VATRows.Insert(new VATRow() { TaxBoxNbr = "5", TaxBoxCode = "netVatDue", Descr = "The difference between Box 3 and Box 4", Amt = vATreturn.netVatDue });
			graph.VATRows.Insert(new VATRow() { TaxBoxNbr = "6", TaxBoxCode = "totalValueSalesExVAT", Descr = " Total value of sales and all other outputs excluding any VAT", Amt = vATreturn.totalValueSalesExVAT });
			graph.VATRows.Insert(new VATRow() { TaxBoxNbr = "7", TaxBoxCode = "totalValuePurchasesExVAT", Descr = "Total value of purchases and all other inputs excluding any VAT", Amt = vATreturn.totalValuePurchasesExVAT });
			graph.VATRows.Insert(new VATRow() { TaxBoxNbr = "8", TaxBoxCode = "totalValueGoodsSuppliedExVAT", Descr = "Total value of all supplies of goods and related costs, excluding any VAT, to other EC member states.", Amt = vATreturn.totalValueGoodsSuppliedExVAT });
			graph.VATRows.Insert(new VATRow() { TaxBoxNbr = "9", TaxBoxCode = "totalAcquisitionsExVAT", Descr = "Total value of acquisitions of goods and related costs excluding any VAT, from other EC member states.", Amt = vATreturn.totalAcquisitionsExVAT });
            graph.VATRows.Insert(new VATRow() { TaxBoxNbr = "Period", TaxBoxCode = "periodKey", Descr = vATreturn.periodKey, Amt = null });
        }

        public void SetVRN(VATPeriodFilter p)
        {
            if (p.OrganizationID == null) return;

            string vrn;
            HMRCOrganizationBAccount org =
                PXSelect<HMRCOrganizationBAccount,
                Where<HMRCOrganizationBAccount.organizationID, Equal<Required<HMRCOrganizationBAccount.organizationID>>>>
                .SelectSingleBound(this, null, p.OrganizationID).FirstOrDefault();

            vrn = org.TaxRegistrationID;
            if (org.FileTaxesByBranches == true && p.BranchID!=null)
            {
                HMRCBranchBAccount branch =
                    PXSelect<HMRCBranchBAccount,
                    Where<HMRCBranchBAccount.branchID, Equal<Required<HMRCBranchBAccount.branchID>>>>
                    .SelectSingleBound(this, null, p.BranchID).FirstOrDefault();

                vrn = branch.TaxRegistrationID;
            }
            VATProvider.setVRN(vrn);
        }

        public static void SendVATReturnProc(VATMaint graph, VATPeriodFilter p, bool finalised=false)
		{
            #region Tax Box
            /*
			Outputs
			Box 1 (vatDueSales) VAT due in the period on sales and other outputs
			Box 2 (vatDueAcquisitions) VAT due in the period on acquisitions from other EU member states
			Box 3 (totalVatDue) Total VAT due (Box 1 + Box 2)

			Inputs
			Box 4 (vatReclaimedCurrPeriod) VAT reclaimed in the period on purchases and other inputs (including acquisitions from the EU)
			Box 5 (netVatDue) net VAT to be paid to HMRC or reclaimed (difference between Box 3 and Box 4)
			Box 6 (totalValueSalesExVAT) total value of sales and all other outputs excluding any VAT
			Box 7 (totalValuePurchasesExVAT) the total value of purchases and all other inputs excluding any VAT
			Box 8 (totalValueGoodsSuppliedExVAT) total value of all supplies of goods and related costs, excluding any VAT, to other EU member states
			Box 9 (totalAcquisitionsExVAT) total value of all acquisitions of goods and related costs, excluding any VAT, from other EU member states 
			*/
            #endregion

            Model.VATreturn ret = new Model.VATreturn() { periodKey = p.PeriodKey, finalised = finalised };
            #region fill report
            decimal amt = 0;
            foreach (PXResult<TaxReportLine, TaxHistoryReleased> res in graph.Period_Details.Select())
            {
                TaxReportLine line = res;
                TaxHistoryReleased hist = res;
                amt = hist.ReportFiledAmt ?? 0;
                switch (line.ReportLineNbr)
                {
                    case "1": ret.vatDueSales = amt; break;
                    case "2": ret.vatDueAcquisitions = amt; break;
                    case "3": ret.totalVatDue = amt; break;
                    case "4": ret.vatReclaimedCurrPeriod = amt; break;
                    case "5": ret.netVatDue = amt; break;
                    case "6": ret.totalValueSalesExVAT = amt; break;
                    case "7": ret.totalValuePurchasesExVAT = amt; break;
                    case "8": ret.totalValueGoodsSuppliedExVAT = amt; break;
                    case "9": ret.totalAcquisitionsExVAT = amt; break;
                }
            }
            #endregion
            try
            {
                VATreturnResponse response=graph.VATProvider.SendReturn(ret);
                PXTrace.WriteInformation(JsonConvert.SerializeObject(response));
            }
            catch (Exceptions.VATAPIInvalidToken eToken)
            {
                PXTrace.WriteError(eToken);
                throw new PXException(Messages.PleaseAuthorize);
            }
            catch (Exceptions.VATAPIException eApi)
            {
                PXTrace.WriteError(eApi);
                if (eApi.Data.Contains("errorJson"))
                {
                    PXTrace.WriteError(eApi.Data["errorJson"].ToString());
                }
                throw eApi;
            }
            catch (Exception e)
            {
                PXTrace.WriteError(e);
                throw e;
            }
            throw new PXException(Messages.VATreturnIsAccepted);
        }

		public void UpdateOAuthToken(OAuthToken o)
		{
			PXUpdate<
				Set<OAuthToken.accessToken, Required<OAuthToken.accessToken>,
				Set<OAuthToken.refreshToken, Required<OAuthToken.refreshToken>,
				Set<OAuthToken.utcExpiredOn, Required<OAuthToken.utcExpiredOn>,
				Set<OAuthToken.bearer, Required<OAuthToken.bearer>>>>>, 
				OAuthToken, 
				Where<OAuthToken.applicationID, Equal<Required<OAuthToken.applicationID>>>>
				.Update(this, 
					o.AccessToken,
					o.RefreshToken,
					o.UtcExpiredOn, 
					o.Bearer, 
					o.ApplicationID);
		}
	}
}
