using System;

namespace Say.Ivona.Model
{
    /// <summary>
    ///     TTS SaaS agreement data
    /// </summary>
    public class AgreementData
    {
        /// <summary>
        ///     <code>true</code> if there is monthly-renewal subscription active for this SaaS agreement, <code>false</code> if
        ///     there is one-use only character pool
        /// </summary>
        public bool IsMonthlyRenewed { get; set; }

        /// <summary>
        ///     all characters from current active agreement, or their monthly limit if monthly renewal is active (value -1 means
        ///     unlimited characters)
        /// </summary>
        public int AllCharacters { get; set; }

        /// <summary>
        ///     the current available characters (value -1 means unlimited characters)
        /// </summary>
        public int CurrentCharacters { get; set; }

        /// <summary>
        ///     the characters available before last operation that changed their value (value -1 means unlimited characters)
        /// </summary>
        public int PreviousCharacters { get; set; }

        /// <summary>
        ///     GMT renewal time for monthly character limit if user has monthly renewal option active in his SaaS agreement
        /// </summary>
        public DateTime? RenewalDate { get; set; }

        /// <summary>
        ///     GMT expiration time for user’s SaaS agreement
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        ///     <code>true</code> if there is a trial version of SaaS currently active, <code>false</code> if there is a full
        ///     version of SaaS active for user’s account
        /// </summary>
        public bool IsTrial { get; set; }

        public AccountLimits Limits { get; set; }

        public class AccountLimits
        {
            /// <summary>
            ///     maximum number of speech files per user
            /// </summary>
            public int MaxNumberOfSpeechFiles { get; set; }

            /// <summary>
            ///     maximum length of a processed text
            /// </summary>
            public int MaxTextLength { get; set; }
        }
    }
}