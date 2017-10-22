using System;

namespace LogXtreme.Extensions {

    /// <summary>    
    /// Refs
    /// https://msdn.microsoft.com/en-us/library/system.random(v=vs.110).aspx    /// 
    /// </summary>
    public static class RandomExtensions {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="length"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns>an array of random integers between nin and max</returns>
        public static int[] GetIntegers(
            this Random generator,
            int length = 1,
            int min = int.MinValue,
            int max = int.MaxValue) {

            var randLock = new Object();

            length = length > 0 ? length : 0;

            var result = new int[length];

            for (int i = 0; i < length; i++) {

                lock (randLock) {
                    result[i] = generator.Next(min, max);
                }
            }

            return result;
        }

        public static double GetFloat(
            this Random generator,
            double min = double.MinValue,
            double max = double.MaxValue) {

            return min + (max - min) * generator.NextDouble();
        }

        public static double[] GetFloats(
            this Random generator,
            int length = 1,
            double max = double.MaxValue,
            double min = double.MinValue) {

            var randLock = new Object();
            double delta = max - min;

            length = length > 0 ? length : 0;

            var result = new double[length];

            for (int i = 0; i < length; i++) {

                lock (randLock) {
                    result[i] = min + delta * generator.NextDouble();
                }
            }

            return result;
        }        
    }
}
