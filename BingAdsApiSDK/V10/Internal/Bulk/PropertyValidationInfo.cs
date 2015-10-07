namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal class PropertyValidationInfo
    {
        public object Value { get; set; }

        public string Name { get; set; }        

        public PropertyValidationInfo(object value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}
