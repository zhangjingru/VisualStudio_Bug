using Possible.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace MVVM.ViewModel
{
    public interface IRecipe
    {
        public bool Selected { get; set; }
        string ToString();
    }

   
    public class Recipe : ViewModelBase, IRecipe
    {
        public Recipe()
        {
            WaferPoints = new WaferPoint[] { };
        }

        public void ParameterRefresh()
        {
            if (OverscanMaxMinField <= 0) OverscanMaxMinField = 100;

            ZeroToMax = MaxToZero = SweepTime / 2;
            NegativeMaxToMax = MaxToNegativeMax = SweepTime;
            SlewRate = (MaximumField - MinimumField) * (OverscanMaxMinField / 100) / SweepTime;
        }
        #region Field Parame
        private float _SweepTime;
        /// <summary>
        /// 扫描时间（s）
        /// </summary>
        public float SweepTime
        {
            get { return _SweepTime; }
            set
            {
                if (SetProperty(ref _SweepTime, value, nameof(SweepTime)))
                    ParameterRefresh();
            }
        }
        private float _FieldSetTime;

        public float FieldSetTime
        {
            get { return _FieldSetTime; }
            set
            {
                if (SetProperty(ref _FieldSetTime, value, nameof(FieldSetTime)))
                    ParameterRefresh();
            }
        }
        private float _IntraHalfLoopWaitTime;
        /// <summary>
        /// 峰值停留时长
        /// </summary>
        public float IntraHalfLoopWaitTime
        {
            get { return _IntraHalfLoopWaitTime; }
            set
            {
                if (SetProperty(ref _IntraHalfLoopWaitTime, value, nameof(IntraHalfLoopWaitTime)))
                    ParameterRefresh();
            }
        }
        private float _OverscanMaxMinField;
        /// <summary>
        /// 过冲补偿
        /// </summary>
        public float OverscanMaxMinField
        {
            get { return _OverscanMaxMinField; }
            set
            {
                if (SetProperty(ref _OverscanMaxMinField, value, nameof(OverscanMaxMinField)))
                    ParameterRefresh();
            }
        }

        private float _MaximumField;
        /// <summary>
        /// T
        /// </summary>
        public float MaximumField
        {
            get { return _MaximumField; }
            set
            {
                if (SetProperty(ref _MaximumField, value, nameof(MaximumField)))
                    ParameterRefresh();
            }
        }
        private float _MinimumField;
        public float MinimumField
        {
            get { return _MinimumField; }
            set
            {
                if (SetProperty(ref _MinimumField, value, nameof(MinimumField)))
                    ParameterRefresh();
            }
        }

        private float _Range;
        public float Range
        {
            get { return _Range; }
            set { SetProperty(ref _Range, value, nameof(Range)); }
        }
        private float _SlewRate;
        public float SlewRate
        {
            get { return _SlewRate; }
            set { SetProperty(ref _SlewRate, value, nameof(SlewRate)); }
        }
        private float _ZeroToMax;
        public float ZeroToMax
        {
            get { return _ZeroToMax; }
            set { SetProperty(ref _ZeroToMax, value, nameof(ZeroToMax)); }
        }
        private float _MaxToNegativeMax;
        public float MaxToNegativeMax
        {
            get { return _MaxToNegativeMax; }
            set { SetProperty(ref _MaxToNegativeMax, value, nameof(MaxToNegativeMax)); }
        }
        private float _NegativeMaxToMax;
        public float NegativeMaxToMax
        {
            get { return _NegativeMaxToMax; }
            set { SetProperty(ref _NegativeMaxToMax, value, nameof(NegativeMaxToMax)); }
        }
        private float _MaxToZero;
        public float MaxToZero
        {
            get { return _MaxToZero; }
            set { SetProperty(ref _MaxToZero, value, nameof(MaxToZero)); }
        }

        private float _OerstedPerV;
        public float OerstedPerV
        {
            get { return _OerstedPerV; }
            set { SetProperty(ref _OerstedPerV, value, nameof(OerstedPerV)); }
        }
        private float _FieldOffset;
        public float FieldOffset
        {
            get { return _FieldOffset; }
            set { SetProperty(ref _FieldOffset, value, nameof(FieldOffset)); }
        }
        #endregion
        #region Wafer Info
        private string _WaferID;
        public string WaferID
        {
            get { return _WaferID; }
            set { SetProperty(ref _WaferID, value, nameof(WaferID)); }
        }

        private WaferTypeEnum _WaferType;
        public WaferTypeEnum WaferType
        {
            get { return _WaferType; }
            set
            {
                SetProperty(ref _WaferType, value, nameof(WaferType));
                switch (value)
                {
                    //case WaferTypeEnum.inches_12:
                    //    WaferDiameter = 300;
                    //    break;
                    case WaferTypeEnum.inches_8:
                        WaferDiameter = 200;
                        break;
                    case WaferTypeEnum.inches_6:
                        WaferDiameter = 150;
                        break;
                    case WaferTypeEnum.Fragments:
                        if (WaferDiameter > 60)
                        {
                            WaferDiameter = 60;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private float _WaferDiameter;
        public float WaferDiameter
        {
            get { return _WaferDiameter; }
            set
            {
                if (SetProperty(ref _WaferDiameter, value, nameof(WaferDiameter)))
                {
                    OnWaferDiameterChangeEnvent(value);
                }
            }
        }
        #endregion

        public WaferPoint[] WaferPoints { get; set; }

        private string _RecipeName;
        public string RecipeName
        {
            get { return _RecipeName; }
            set { SetProperty(ref _RecipeName, value, nameof(RecipeName)); }
        }
        private bool _Selected;
        public bool Selected
        {
            get { return _Selected; }
            set { SetProperty(ref _Selected, value, nameof(Selected)); }
        }
        public void OnWaferDiameterChangeEnvent(float WaferDiameter)
        {
            OnWaferDiameterChange?.Invoke(null, WaferDiameter);
        }
        [field: NonSerialized()]
        public event EventHandler<float> OnWaferDiameterChange;
        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return RecipeName;
        }


        public bool ParameCheck(out string ErrInfo)
        {
            ErrInfo = "";
            if (SweepTime <= 0)
            {
                //ErrInfo = "Sweep Time cannot be less than or equal to 0";
                ErrInfo = "Please enter the correct \"Sweep Time\"";
                return false;
            }
            if (MaximumField == 0 && MinimumField == 0)
            {
                ErrInfo = "Please enter the correct \"MaximumField\" \"MinimumField\"";
                return false;
            }

            if (WaferDiameter <= 0)
            {
                ErrInfo = "Please enter the correct \"Wafer Diameter\"";
                return false;
            }
            if (WaferPoints.Length <= 0)
            {
                ErrInfo = "Please enter the Points you want to detect in \"Points to Detect\"";
                return false;
            }

            return true;
        }
    }
    [Serializable]
    public class RecipeGroup : ViewModelBase, IRecipe
    {
        public RecipeGroup()
        {
            Recipes = new List<Recipe>();
        }
        private bool _Selected;
        public bool Selected
        {
            get { return _Selected; }
            set { SetProperty(ref _Selected, value, nameof(Selected)); }
        }
        public List<Recipe> Recipes { get; }
        public string GroupName { get; set; }
        public override string ToString()
        {
            return $"{GroupName}->{{{String.Join(" ", Recipes)}}}";
        }
    }

    public enum WaferTypeEnum
    {
        //[Description("12 inches")]
        //[WaferDiameter(300)]
        //inches_12,

        [Description("8 inches")]
        [WaferDiameter(200)]
        inches_8,

        [Description("6 inches")]
        [WaferDiameter(150)]
        inches_6,

        [Description("Fragments")]
        [WaferDiameter(0)]
        Fragments,
    }
    [AttributeUsage(AttributeTargets.Field)]
    public class WaferDiameter : System.Attribute
    {
        public float Diameter { get; }

        public WaferDiameter(float Diameter)
        {
            this.Diameter = Diameter;
        }
    }
}
