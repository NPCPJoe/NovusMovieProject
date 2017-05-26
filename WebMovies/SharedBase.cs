using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ddl = ApplicationVariables.ApplicationVariables.SystemValues.DropDownLists;

namespace WebMovies
{
    public class SharedBase : Page 
    {
        protected void populateDropDownList<T>(bool addBlankItem, string controlID, List<T> datasource, string dataTextField, string dataValueField)
        { // populates the drop down list.
            DropDownList ddl = Page.FindControl(controlID) as DropDownList;
            ddl.DataTextField = dataTextField;
            ddl.DataValueField = dataValueField;
            ddl.DataSource = datasource;
            ddl.DataBind();
            if (datasource.Count > 1 && addBlankItem) { addBlankItemToDropDownList(ref ddl); }
        }

        protected void addBlankItemToDropDownList(ref DropDownList ddlist)
        {// blanks items to the drop down list.
            ddlist.Items.Insert(0, new ListItem(ddl.DefaultText, ddl.DefaultValue));
            ddlist.SelectedIndex = 0;
        }

    }
}