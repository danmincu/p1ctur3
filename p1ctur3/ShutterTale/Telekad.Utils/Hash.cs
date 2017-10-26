using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Telekad.Utils
{
    /// <summary>
    /// Type to be used in every class where we need to override GetHashCode() method
    /// Two typical use cases are:
    /// 
    /// 1) If we need to override GetHashCode() but want to only do hash calculation once 
    /// the first time this method has been called. In this case:
    ///     a) add member variable of the Hash type
    ///     b) initialize it in the counstructor providing recalculateAlways = false
    ///     c) Inside the GetHashCode(), call Calculate( array-of-objects ) providing the array of objects to use for hashing and then: return hash.Value
    ///     
    /// 2) We want instant (inline) hash calculation:
    ///     a) use static method: Hash.GetHash(array-of-objects).Value
    ///     
    /// </summary>
    public class Hash
    {
        #region Fields

        private const PrimeNumber defaultPrime1 = PrimeNumber.Prime11;
        private const PrimeNumber defaultPrime2 = PrimeNumber.Prime31;
        private object syncRoot;
        private int? hashIntValue;

        #endregion

        #region Properties

        public int Value
        {
            get
            {
                lock (this.syncRoot)
                {
                    if (this.hashIntValue == null) 
                        return 0;
                    return (int)hashIntValue;
                }
            }
        }
        public PrimeNumber Prime1 { get; private set; }
        public PrimeNumber Prime2 { get; private set; }
        public bool RecalculateAlways { get; private set; }
        public bool IsCalculated { get { return this.hashIntValue != null; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor assumes default prime numbers and will always be recalculated
        /// </summary>
        public Hash()
            : this(true)
        { }

        /// <summary>
        /// Constructor assumes default prime numbers.
        /// </summary>
        /// <param name="recalculateAlways">determines if the Calculate will always perform new calculation</param>
        public Hash(bool recalculateAlways)
            : this(recalculateAlways, defaultPrime1, defaultPrime2)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recalculateAlways">determines if the Calculate will always perform new calculation</param>
        /// <param name="prime1">First prime number to be used in the calculation algorithm</param>
        /// <param name="prime2">Second prime number to be used in the calculation algorithm</param>
        public Hash(bool recalculateAlways, PrimeNumber prime1, PrimeNumber prime2)
        {
            this.Prime1 = prime1;
            this.Prime2 = prime2;
            this.hashIntValue = null;
            this.syncRoot = new object();
            this.RecalculateAlways = recalculateAlways;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Using supplied objects, determines the value for the Hash.
        /// If RecalculateAlways is set to False, Calculate will do nothing 
        /// if HashIntValue is already determined.
        /// </summary>
        /// <param name="input">list of fields, properties etc to use for hash creation</param>
        public void Calculate(params object[] input)
        {
            lock (this.syncRoot)
            {
                if (this.RecalculateAlways || this.hashIntValue == null)
                {
                    unchecked
                    {
                        this.hashIntValue = (int)Prime1;

                        for (int i = 0; i < input.Length; i++)
                        {
                            this.hashIntValue = this.hashIntValue * ((int)this.Prime2) + (input[i] == null ? 0 : input[i].GetHashCode());
                        }
                    }
                }
            }
        }

        #endregion

        #region Static utility methods

        /// <summary>
        /// Utility method. Enables quick hash creation. 
        /// Hash returned will have RecalcualteAlways set to true.
        /// </summary>
        /// <param name="prime1">First prime value to use in hashing algorithm</param>
        /// <param name="prime2">Second prime value to use in hashing algorithm</param>/// 
        /// <param name="input">list of fields, properties etc to use for hash creation</param>
        /// <returns>Hash object</returns>
        public static Hash GetHash(PrimeNumber prime1, PrimeNumber prime2, params object[] input)
        {
            Hash hc = new Hash(true, prime1, prime2);
            hc.Calculate(input);
            return hc;
        }

        /// <summary>
        /// Utility method. Enables quick hash creation. Default prime numbers used. 
        /// Hash returned will have RecalcualteAlways set to true.
        /// </summary>
        /// <param name="input">list of fields, properties etc to use for hash creation</param>
        /// <returns>Hash object</returns>
        public static Hash GetHash(params object[] input)
        {
            return GetHash(defaultPrime1, defaultPrime2, input);
        }

        #endregion
    }

    // List of Prime numbers up to 1000
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    public enum PrimeNumber : int
    {
        Prime2 = 2,
        Prime3 = 3,
        Prime5 = 5,
        Prime7 = 7,
        Prime11 = 11,
        Prime13 = 13,
        Prime17 = 17,
        Prime19 = 19,
        Prime23 = 23,
        Prime29 = 29,
        Prime31 = 31,
        Prime37 = 37,
        Prime41 = 41,
        Prime43 = 43,
        Prime47 = 47,
        Prime53 = 53,
        Prime59 = 59,
        Prime61 = 61,
        Prime67 = 67,
        Prime71 = 71,
        Prime73 = 73,
        Prime79 = 79,
        Prime83 = 83,
        Prime89 = 89,
        Prime97 = 97,
        Prime101 = 101,
        Prime103 = 103,
        Prime107 = 107,
        Prime109 = 109,
        Prime113 = 113,
        Prime127 = 127,
        Prime131 = 131,
        Prime137 = 137,
        Prime139 = 139,
        Prime149 = 149,
        Prime151 = 151,
        Prime157 = 157,
        Prime163 = 163,
        Prime167 = 167,
        Prime173 = 173,
        Prime179 = 179,
        Prime181 = 181,
        Prime191 = 191,
        Prime193 = 193,
        Prime197 = 197,
        Prime199 = 199,
        Prime211 = 211,
        Prime223 = 223,
        Prime227 = 227,
        Prime229 = 229,
        Prime233 = 233,
        Prime239 = 239,
        Prime241 = 241,
        Prime251 = 251,
        Prime257 = 257,
        Prime263 = 263,
        Prime269 = 269,
        Prime271 = 271,
        Prime277 = 277,
        Prime281 = 281,
        Prime283 = 283,
        Prime293 = 293,
        Prime307 = 307,
        Prime311 = 311,
        Prime313 = 313,
        Prime317 = 317,
        Prime331 = 331,
        Prime337 = 337,
        Prime347 = 347,
        Prime349 = 349,
        Prime353 = 353,
        Prime359 = 359,
        Prime367 = 367,
        Prime373 = 373,
        Prime379 = 379,
        Prime383 = 383,
        Prime389 = 389,
        Prime397 = 397,
        Prime401 = 401,
        Prime409 = 409,
        Prime419 = 419,
        Prime421 = 421,
        Prime431 = 431,
        Prime433 = 433,
        Prime439 = 439,
        Prime443 = 443,
        Prime449 = 449,
        Prime457 = 457,
        Prime461 = 461,
        Prime463 = 463,
        Prime467 = 467,
        Prime479 = 479,
        Prime487 = 487,
        Prime491 = 491,
        Prime499 = 499,
        Prime503 = 503,
        Prime509 = 509,
        Prime521 = 521,
        Prime523 = 523,
        Prime541 = 541,
        Prime547 = 547,
        Prime557 = 557,
        Prime563 = 563,
        Prime569 = 569,
        Prime571 = 571,
        Prime577 = 577,
        Prime587 = 587,
        Prime593 = 593,
        Prime599 = 599,
        Prime601 = 601,
        Prime607 = 607,
        Prime613 = 613,
        Prime617 = 617,
        Prime619 = 619,
        Prime631 = 631,
        Prime641 = 641,
        Prime643 = 643,
        Prime647 = 647,
        Prime653 = 653,
        Prime659 = 659,
        Prime661 = 661,
        Prime673 = 673,
        Prime677 = 677,
        Prime683 = 683,
        Prime691 = 691,
        Prime701 = 701,
        Prime709 = 709,
        Prime719 = 719,
        Prime727 = 727,
        Prime733 = 733,
        Prime739 = 739,
        Prime743 = 743,
        Prime751 = 751,
        Prime757 = 757,
        Prime761 = 761,
        Prime769 = 769,
        Prime773 = 773,
        Prime787 = 787,
        Prime797 = 797,
        Prime809 = 809,
        Prime811 = 811,
        Prime821 = 821,
        Prime823 = 823,
        Prime827 = 827,
        Prime829 = 829,
        Prime839 = 839,
        Prime853 = 853,
        Prime857 = 857,
        Prime859 = 859,
        Prime863 = 863,
        Prime877 = 877,
        Prime881 = 881,
        Prime883 = 883,
        Prime887 = 887,
        Prime907 = 907,
        Prime911 = 911,
        Prime919 = 919,
        Prime929 = 929,
        Prime937 = 937,
        Prime941 = 941,
        Prime947 = 947,
        Prime953 = 953,
        Prime967 = 967,
        Prime971 = 971,
        Prime977 = 977,
        Prime983 = 983,
        Prime991 = 991,
        Prime997 = 997
    }
}
