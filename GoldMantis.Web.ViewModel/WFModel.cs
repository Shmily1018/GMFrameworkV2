using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Common;

namespace GoldMantis.Web.ViewModel
{
    [DataContract]
    public class WFModel : BaseModel
    {
        [DataMember]
        public bool IsSave { get; set; }

        [DataMember]
        public Guid ActorTokenId { get; set; }

        [DataMember]
        public Guid WFPublishID { get; set; }

        [DataMember]
        public string WFName { get; set; }

        [DataMember]
        public string NodeName { get; set; }

        [DataMember]
        public bool IsEmergency { get; set; }

        [DataMember]
        public bool EnableWorkFlow { get; set; }

        [DataMember]
        public virtual EnumCheckStatus FormCheckStatus { get; set; }

        [DataMember]
        public bool AllowCustomFlow { get; set; }

        [DataMember]
        public bool UseMasterProcessSet { get; set; }

        [DataMember]
        public string Idea { get; set; }

        [DataMember]
        public bool IsDirty { get; set; }

        [DataMember]
        public string CurrentCommand { get; set; }

        [DataMember]
        public bool IsExecuteJSAfterFailedSubmit { get; set; }

        [DataMember]
        public bool IsCustomFlow { get; set; }

        [DataMember]
        public virtual int KeyID { get; set; }

        [DataMember]
        public virtual int DeptID { get; set; }

        [DataMember]
        public virtual int? CheckStatus { get; set; }

        [DataMember]
        public int CurrentUser { get; set; }

        [DataMember]
        public string TokenExtendInfo { get; set; }

        [DataMember]
        public string WFTokenName { get; set; }

        [DataMember]
        public string MenuID { get; set; }

        [DataMember]
        public string MenuURL { get; set; }

        [DataMember]
        public string SelectNextID { get; set; }

        [DataMember]
        public string SelectActivityID { get; set; }

        [DataMember]
        public int CurrentProjectID { get; set; }

        [DataMember]
        public string InputKey { get; set; }

        [DataMember]
        public string SelectUserID { get; set; }

        [DataMember]
        public string ExecuteJS { get; set; }

        [DataMember]
        public string IsDialog { get; set; }

        [DataMember]
        public bool IsShowConfirm { get; set; }

        [DataMember]
        public string ReStartCommand { get; set; }

        [DataMember]
        public string Comand { get; set; }

        [DataMember]
        public string PageClass { get; set; }

        [DataMember]
        public string SubmitPrefix { get; set; }

        


        #region button visible

        [DataMember]
        public bool ButtonSaveVisible { get; set; }

        [DataMember]
        public bool ButtonSubmitVisible { get; set; }

        [DataMember]
        public bool ButtonUnSubmitVisible { get; set; }

        [DataMember]
        [DefaultValue(true)]
        public bool ButtonReSubmitVisible { get; set; }

        [DataMember]
        public bool ButtonBackVisible { get; set; }

        [DataMember]
        public bool ButtonThroughVisible { get; set; }

        [DataMember]
        public bool ButtonAgreeVisible { get; set; }

        [DataMember]
        public bool ButtonDeprecateVisible { get; set; }

        [DataMember]
        public bool ButtonCCVisible { get; set; }

        [DataMember]
        public bool ButtonCancelVisible { get; set; }

        [DataMember]
        public bool ButtonStopVisible { get; set; }

        [DataMember]
        public bool ButtonViewVisible { get; set; }

        [DataMember]
        public bool ButtonAssignVisible { get; set; }

        [DataMember]
        public bool ButtonSuspendVisible { get; set; }

        [DataMember]
        public bool ButtonResumeVisible { get; set; }

        [DataMember]
        public bool ButtonViewOpinionVisible { get; set; }

        [DataMember]
        public bool ButtonViewDiagramVisible { get; set; }

        [DataMember]
        public bool ButtonCustomFlowVisible { get; set; }

        #endregion
    }
}
