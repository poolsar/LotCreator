using HtmlAgilityPack;

namespace PrestaWinClient
{
 //   public static class HtmlNodeExtensions
 //   {
 //       public static string TextByClass(this HtmlNode node, string className)
 //       {
 //           var xpath = string.Format("//*[contains(@class,'{0}')]", className);
 //           var text = node.TextByXpath(xpath);
 //           return text;
 //       }

 //       public static string TextByXpath(this HtmlNode node, string xpath)
 //       {
 //           HtmlNode htmlNode = node.NodeByXpath(xpath);

 //           string text = htmlNode == null ? string.Empty : htmlNode.InnerText.Replace("&#160;", " ").Trim();
 //           return text;
 //       }

 //       public static decimal? PriceByXpath(this HtmlNode node, string xpath, string rubls = null)
 //       {
 //           HtmlNode htmlNode = node.NodeByXpath(xpath);


 //           if (htmlNode == null) return null;

 //           string text = htmlNode.InnerText.Replace("&#160;", " ");
            
 //           if(!string.IsNullOrWhiteSpace(rubls))
 //                   text=text.Replace(rubls, "").Trim();
            
 //           text = text.Trim();
            
 //           decimal price;

 //           bool parsed = decimal.TryParse(text, out price);

 //           if (!parsed) return null;

 //           return price;

 //       }

 //       public static HtmlNode NodeByClass(this HtmlNode node, string className)
 //       {
 //           var xpath = string.Format("//*[contains(@class,'{0}')]", className);
 //           node = node.NodeByXpath(xpath);
 //           return node;
 //       }

 //       //public static HtmlNode NodeByClass(this HtmlNode node, string className)
 //       //{
 //       //    var xpath = string.Format(".//*[contains(@class,'{0}')]", className);
 //       //    var htmlNode = node.SelectSingleNode(xpath);

 //       //    return htmlNode;
 //       //}

 //       public static HtmlNode NodeByXpath(this HtmlNode
 //node, string xpath)
 //       {

 //           node = node.SelectSingleNode(xpath);
 //           return node;
 //       }

 //       //StringBuilder _log = new StringBuilder();

 //       //public string ReadLog()
 //       //{
 //       //    string logStr = _log.ToString();
 //       //    return logStr;
 //       //}

 //       //public void WriteLog(string str, params object[] pObjects)
 //       //{

 //       //    //sbLog.AppendLine(string.Format(str, pObjects));

 //       //    string message = string.Format(str, pObjects);

 //       //    _log.AppendLine(message);

 //       //    //_writeOutput(message);
 //       //    //DoOnForm(f => f.WriteOutput(message));
 //       //}
 //   }
}