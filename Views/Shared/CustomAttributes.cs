namespace System.ComponentModel.DataAnnotations
{
    public class FutureDateAttribute : RangeAttribute
    {
        public FutureDateAttribute() : base(typeof(DateTime),
                                            DateTime.Now.ToString("MM/dd/yyyy"), 
                                            DateTime.MaxValue.ToString("MM/dd/yyyy"))
        {}
    }
}