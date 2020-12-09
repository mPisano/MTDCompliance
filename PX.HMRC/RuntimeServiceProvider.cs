using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace PX.HMRC
{
    // This allows us to use the Windows Collection Editor outside of the Properties Control

    public class ColEditor<T>
    {
        public DialogResult ShowDialog(T lst)
        {
            CollectionEditor c = new CollectionEditor(typeof(T));
            RuntimeServiceProvider serviceProvider = new RuntimeServiceProvider();
            c.EditValue(serviceProvider, serviceProvider, lst);
            return serviceProvider.windowsFormsEditorService.DialogResult;
        }
    }


    public class RuntimeServiceProvider : IServiceProvider, ITypeDescriptorContext
    {
        #region IServiceProvider Members
        public WindowsFormsEditorService windowsFormsEditorService;

        object IServiceProvider.GetService(Type serviceType)
        {
            if (serviceType == typeof(IWindowsFormsEditorService))
            {
                windowsFormsEditorService = new WindowsFormsEditorService();
                return windowsFormsEditorService;
            }

            return null;
        }

        public class WindowsFormsEditorService : IWindowsFormsEditorService
        {
            #region IWindowsFormsEditorService Members
            public DialogResult DialogResult = DialogResult.None;

            public void DropDownControl(Control control)
            {
            }

            public void CloseDropDown()
            {
            }

            public System.Windows.Forms.DialogResult ShowDialog(Form dialog)
            {
                ((System.Windows.Forms.Button)dialog.Controls.Find("okButton", true)[0]).Click += WindowsFormsEditorService_Click;
                return dialog.ShowDialog();
            }
            private void WindowsFormsEditorService_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.OK;
            }
            #endregion
        }

        #endregion

        #region ITypeDescriptorContext Members

        public void OnComponentChanged()
        {
        }

        public IContainer Container
        {
            get { return null; }
        }

        public bool OnComponentChanging()
        {
            return true; // true to keep changes, otherwise false
        }

        public object Instance
        {
            get { return null; }
        }

        public PropertyDescriptor PropertyDescriptor
        {
            get { return null; }
        }

        #endregion
    }
}
