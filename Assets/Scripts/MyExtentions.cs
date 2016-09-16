namespace ExtentionMethods
{
    /// <summary>
    /// Extentions used for differenct calculations.
    /// </summary>
    public static class MyExtentions {

        //returns the awnser of the sum of all numbers between 1 and value, if value is 4: awnser = 4+3+2+1 = 10.
        public static float SumofAllIntegersBetweeOneAndValue(this float value)
        {
            return value * (value + 1) / 2;
        }
        //returns the mutification of value with itself.
        public static float PowerOfTwo(this float value)
        {
            return value * value;
        }
    }
}
