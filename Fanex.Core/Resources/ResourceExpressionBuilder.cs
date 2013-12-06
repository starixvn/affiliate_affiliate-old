// -----------------------------------------------------------------------
// <copyright file="ResourceExpressionBuilder.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Core.Resources
{
    using System.CodeDom;
    using System.Web.Compilation;
    using System.Web.UI;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ExpressionPrefix("Res")]
    public class CodeExpressionBuilder : ExpressionBuilder
    {
        public override CodeExpression GetCodeExpression(BoundPropertyEntry entry,
           object parsedData, ExpressionBuilderContext context)
        {
            return new CodeSnippetExpression(entry.Expression);
        }
    }

}
