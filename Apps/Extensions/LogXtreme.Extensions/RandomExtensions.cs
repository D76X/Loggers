using System;

namespace LogXtreme.Extensions {

    /// <summary>    /// 
    /// Refs
    /// https://msdn.microsoft.com/en-us/library/system.random(v=vs.110).aspx    /// 
    /// </summary>
    public static class RandomExtensions {

        public static int[] Sequence(
            this Random generator, 
            int length, 
            int max, 
            int min = 0) {

            var randLock = new Object();

            length = length >= 0 ? length : 0;           

            var result = new int[length];

            for (int i = 0; i < length; i++) {

                lock (randLock) {
                    result[i] = generator.Next(min, max);
                }                
            }

            return result;
        }      
    }
}
