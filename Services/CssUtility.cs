namespace CssUtility
{
    public class CircleUtilities
    {
        public double W_Container { get; set; }
        public double H_Container { get; set; }
        public double W_Item { get; set; }
        public double H_Item { get; set; }
        public CircleUtilities(double w_Container, double h_Container, double w_Item, double h_Item)
        {
            W_Container = w_Container;
            H_Container = h_Container;
            W_Item = w_Item;
            H_Item = h_Item;
        }
        /// <summary>
        /// Get Width ofset value for item , the unit is percent
        /// </summary>
        /// <param name="angle"> unit is degree</param>
        /// <returns>the offset W value</returns>
        public double GetWOffsetForItemFromDeg(double degree)
        {

            double x = (1 / 2) * (1 + Math.Cos(degree * (Math.PI / 180)) - W_Item / (2*W_Container));
            return x * 100;
        }
        /// <summary>
        /// Get Width ofset value for item , the unit is percent
        /// </summary>
        /// <param name="angle"> unit is degree</param>
        /// <returns>the offset H value</returns>

        public double GetHOffsetForItemFromDeg(double degree)
        {

             double x = (1 / 2) * (1 - Math.Sin(degree * (Math.PI / 180)) - H_Item / (2*H_Container));
            return x * 100;
        }
        
    }
}