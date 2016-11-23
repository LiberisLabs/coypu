using System;
using System.Linq;

namespace Coypu.Drivers
{
    /// <summary>
    /// Helper for formatting XPath queries
    /// </summary>
    public class XPath
    {
        private readonly bool _uppercaseTagNames;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uppercaseTagNames"></param>
        public XPath(bool uppercaseTagNames = false)
        {
            _uppercaseTagNames = uppercaseTagNames;
        }

        /// <summary>
        /// <para>Format an XPath query that uses string values for comparison that may contain single or double quotes</para>
        /// <para>Wraps the string in the appropriate quotes or uses concat() to separate them if both are present.</para>
        /// <para>Usage:</para>
        /// <code>  new XPath().Format(".//element[@attribute1 = {0} and @attribute2 = {1}]",inputOne,inputTwo) </code>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Format(string value, params object[] args)
        {
            return string.Format(value, args.Select(a => Literal(a.ToString())).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        public const string and = " and ";
        /// <summary>
        /// 
        /// </summary>
        public const string or = " or ";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string Group(string expression)
        {
            return "(" + expression + ")";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string And(string expression)
        {
            return and + Group(expression);
        }

        internal string Literal(string value)
        {
            if (HasNoDoubleQuotes(value))
                return WrapInDoubleQuotes(value);

            if (HasNoSingleQuotes(value))
                return WrapInSingleQuote(value);

            return BuildConcatSeparatingSingleAndDoubleQuotes(value);
        }

        private string BuildConcatSeparatingSingleAndDoubleQuotes(string value)
        {
            var doubleQuotedParts = value.Split('\"')
                                         .Select(WrapInDoubleQuotes)
                                         .ToArray();

            var reJoinedWithDoubleQuoteParts = String.Join(", '\"', ", doubleQuotedParts);

            return $"concat({TrimEmptyParts(reJoinedWithDoubleQuoteParts)})";
        }

        private static string WrapInSingleQuote(string value)
        {
            return $"'{value}'";
        }

        private static string WrapInDoubleQuotes(string value)
        {
            return $"\"{value}\"";
        }

        private static string TrimEmptyParts(string concatArguments)
        {
            return concatArguments.Replace(", \"\"", "")
                                  .Replace("\"\", ", "");
        }

        private static bool HasNoSingleQuotes(string value)
        {
            return !value.Contains("'");
        }

        private static bool HasNoDoubleQuotes(string value)
        {
            return !value.Contains("\"");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classNames"></param>
        /// <returns></returns>
        public string HasOneOfClasses(params string[] classNames)
        {
            return Group(string.Join(" or ", classNames.Select(XPathNodeHasClass).ToArray()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public string XPathNodeHasClass(string className)
        {
            return $"contains(@class,' {className}') " + $"or contains(@class,'{className} ') " + $"or contains(@class,' {className} ')";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string AttributeIsOneOfOrMissing(string attributeName, string[] values)
        {
            return Group(AttributeIsOneOf(attributeName, values) + or + "not(@" + attributeName + ")");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string AttributeIsOneOf(string attributeName, string[] values)
        {
            return Group(string.Join(" or ", values.Select(t => Format("@" + attributeName + " = {0}", t)).ToArray()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string Attr(string name, string value, Options options)
        {
            return Is("@" + name, value, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldTagNames"></param>
        /// <returns></returns>
        public string TagNamedOneOf(params string[] fieldTagNames)
        {
            return Group(string.Join(" or ", fieldTagNames.Select(t => Format("name() = {0}", CasedTagName(t))).ToArray()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public string CasedTagName(string tagName)
        {
            return _uppercaseTagNames ? tagName.ToUpper() : tagName;
            ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="options"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public string AttributesMatchLocator(string locator, Options options, params string[] attributes)
        {
            return Group(string.Join(" or ", attributes.Select(a => Is(a, locator, options)).ToArray()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string IsContainerLabeled(string locator, Options options)
        {
            return Format("ancestor::label[" + IsTextShallow(locator, options) + "]", locator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string IsForLabeled(string locator, Options options)
        {
            return Format(" (@id = //label[" + IsText(locator, options) + "]/@for) ", locator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="locator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string Is(string selector, string locator, Options options)
        {
            return options.TextPrecisionExact
                ? Format(selector + " = {0} ", locator)
                : Format("contains(" + selector + ",{0})", locator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string IsText(string locator, Options options)
        {
            return Is("normalize-space()", locator, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string IsTextShallow(string locator, Options options)
        {
            return Is("normalize-space(text())", locator, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public string Descendent(string tagName = "*")
        {
            return ".//" + tagName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public string Child(string tagName = "*")
        {
            return "./" + tagName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static string Where(string predicate)
        {
            return "[" + predicate + "]";
        }
    }
}