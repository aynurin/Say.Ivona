using System;
using System.Globalization;

namespace Say.Ivona.Model
{
    public class PronunciationRule
    {
        /// <summary>
        ///     rule identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     type of substitution (1 – simple substitutions, case insensitive, 2 – simple substitutions, case sensitive, 3 –
        ///     regular expressions (currently not available – will be available soon))
        /// </summary>
        public int Stat { get; set; }

        /// <summary>
        ///     pattern to search ("from" part of replacement)
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     replacement value ("to" part of replacement)
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     the language this rule is bound to
        /// </summary>
        public string LangId { get; set; }

        internal Params AddRuleHttpParams()
        {
            if (String.IsNullOrEmpty(LangId))
                throw new ArgumentException("LangId must be set");
            if (String.IsNullOrEmpty(Key))
                throw new ArgumentException("Key must be set");
            if (String.IsNullOrEmpty(Value))
                throw new ArgumentException("Value must be set");
            if (Stat < 1 || Stat > 3)
                throw new ArgumentException("Stat has to be 1, 2, or 3");

            return new Params
            {
                {"langId", LangId},
                {"stat", Stat.ToString(CultureInfo.InvariantCulture)},
                {"key", Key},
                {"value", Value}
            };
        }

        internal Params ModifyRuleHttpParams()
        {
            if (Id < 1)
                throw new ArgumentException("Id has to be a valid rule id");

            Params param = AddRuleHttpParams();
            param.Add("id", Id.ToString(CultureInfo.InvariantCulture));

            return param;
        }

        internal Params DeleteRuleHttpParams()
        {
            if (String.IsNullOrEmpty(LangId))
                throw new ArgumentException("LangId must be set");
            if (Id < 1)
                throw new ArgumentException("Id has to be a valid rule id");

            return new Params {{"langId", LangId}, {"id", Id.ToString(CultureInfo.InvariantCulture)}};
        }
    }
}