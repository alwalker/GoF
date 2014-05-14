using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using ViewState;


/// <summary>
/// Base class to all pages in the web site. Provides functionality that is 
/// shared among all pages. Functionality offered includes: page render timing, 
/// gridview sorting, shopping cart access, viewstate provider access, and 
/// Javascript to be sent to the browser.
/// </summary>
/// <remarks>
/// GoF Design Patterns: Template Method.
/// Enterprise Design Patterns: Page Controller. 
/// 
/// The Template Methods Design Pattern is implemented by the StartTime property and 
/// virtual (Overridable in VB) PageRenderTime property.  Each page that derives from this 
/// base class has the option to use the properties as is, or override it with its own 
/// implementation. This base class provides the template for the page render timing facility. 
/// 
/// The Page Controller Pattern is implemented by default in ASP.NET: each page has its own 
/// Page Controller in the form of the code behind page. The implementation of a BasePage allows 
/// the common code that is shared among Page Controllers (pages) to reside in a single 
/// location. That location is this PageBase class. 
/// </remarks>
public class PageBase : Page
{
    // Gets image service url from web.config
    protected static string imageService = ConfigurationManager.AppSettings.Get("ImageService");

    #region Page Render Timing

    // Page render performance fields.
    private DateTime startTime = DateTime.Now;
    private TimeSpan renderTime;

    /// <summary>
    /// Sets and gets the page render starting time. This property 
    /// represents the Template Design Pattern.
    /// </summary>
    public DateTime StartTime
    {
        set { startTime = value; }
        get { return startTime; }
    }

    /// <summary>
    /// Gets page render time. This property is virtual therefore getting the 
    /// page performance is overridable by derived pages. This property 
    /// represents the Template Design Pattern.
    /// </summary>
    public virtual string PageRenderTime
    {
        get
        {
            renderTime = DateTime.Now - startTime;
            return renderTime.Seconds + "." + renderTime.Milliseconds + " seconds";
        }
    }

    #endregion

    #region Sorting support

    /// <summary>
    /// Adds an up- or down-arrow image to GridView header.
    /// </summary>
    /// <param name="grid">Gridview.</param>
    /// <param name="row">Header row of gridview.</param>
    protected void AddGlyph(GridView grid, GridViewRow row)
    {
        // Find the column sorted on
        for (int i = 0; i < grid.Columns.Count; i++)
        {
            if (grid.Columns[i].SortExpression == SortColumn)
            {
                // Add a space between header and symbol
                Literal literal = new Literal();
                literal.Text = "&nbsp;";
                row.Cells[i].Controls.Add(literal);

                Image image = new Image();
                image.ImageUrl = (SortDirection == "ASC" ?
                    "~/Images/Site/sortasc.gif" :
                    "~/Images/Site/sortdesc.gif");
                image.Width = 9;
                image.Height = 5;

                row.Cells[i].Controls.Add(image);

                return;
            }
        }
    }

    /// <summary>
    /// Gets or sets the current column being sorted on.
    /// </summary>
    protected string SortColumn
    {
        get { return ViewState["SortColumn"].ToString(); }
        set { ViewState["SortColumn"] = value; }
    }

    /// <summary>
    /// Gets or sets the current sort direction (ascending or descending).
    /// </summary>
    protected string SortDirection
    {
        get { return ViewState["SortDirection"].ToString(); }
        set { ViewState["SortDirection"] = value; }
    }

    /// <summary>
    /// Gets the Sql sort expression for the current sort settings.
    /// </summary>
    protected string SortExpression
    {
        get { return SortColumn + " " + SortDirection; }
    }
    #endregion

    #region ViewState Provider Service Access

    // Random number generator 
    private static Random _random = new Random(Environment.TickCount);

    /// <summary>
    /// Saves any view and control state to appropriate viewstate provider.
    /// This method shields the client from viewstate key generation issues.
    /// </summary>
    /// <param name="viewState"></param>
    protected override void SavePageStateToPersistenceMedium(object viewState)
    {
        // Make up a unique name
        string random = _random.Next(0, int.MaxValue).ToString();
        string name = "ACTION_" + random + "_" + Request.UserHostAddress + "_" + DateTime.Now.Ticks.ToString();

        ViewStateProviderService.SavePageState(name, viewState);
        ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", name);
    }

    ///// <summary>
    ///// Retrieves viewstate from appropriate viewstate provider.
    ///// This method shields the client from viewstate key retrieval issues.
    ///// </summary>
    ///// <returns></returns>
    protected override object LoadPageStateFromPersistenceMedium()
    {
        string name = Request.Form["__VIEWSTATE_KEY"];
        return ViewStateProviderService.LoadPageState(name);
    }

    #endregion

    #region Javascript support

    /// <summary>
    /// Adds an 'Open Window' Javascript function to page.
    /// </summary>
    protected void RegisterOpenWindowJavaScript()
    {
        string script =
          "<script language='JavaScript' type='text/javascript'>" + "\r\n" +
           " <!--" + "\r\n" +
           " function openwindow(url,name,width,height) " + "\r\n" +
           " { " + "\r\n" +
           "   window.open(url,name,'toolbar=yes,location=yes,scrollbars=yes,status=yes,menubar=yes,resizable=yes,top=50,left=50,width='+width+',height=' + height) " + "\r\n" +
           " } " + "\r\n" +
           " //--> " + "\r\n" +
          "</script>" + "\r\n";

        ClientScript.RegisterClientScriptBlock(typeof(string), "OpenWindowScript", script);
    }
    #endregion
}
