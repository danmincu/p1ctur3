<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Media.aspx.cs" Inherits="Media" Title="Media List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Media List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="MediaDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Media.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="[Id]" ReadOnly="True" />
				<asp:BoundField DataField="CaptureDateTime" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Capture Date Time" SortExpression="[CaptureDateTime]"  />
				<asp:BoundField DataField="FileDateTime" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="File Date Time" SortExpression="[FileDateTime]"  />
				<asp:BoundField DataField="ImportDateTime" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Import Date Time" SortExpression="[ImportDateTime]"  />
				<asp:BoundField DataField="Location" HeaderText="Location" SortExpression="[Location]"  />
				<asp:BoundField DataField="Pixelx" HeaderText="Pixelx" SortExpression="[PixelX]"  />
				<asp:BoundField DataField="Pixely" HeaderText="Pixely" SortExpression="[PixelY]"  />
				<asp:BoundField DataField="Dpi" HeaderText="Dpi" SortExpression="[Dpi]"  />
				<asp:BoundField DataField="ContentType" HeaderText="Content Type" SortExpression="[ContentType]"  />
				<asp:BoundField DataField="Make" HeaderText="Make" SortExpression="[Make]"  />
				<asp:BoundField DataField="Model" HeaderText="Model" SortExpression="[Model]"  />
				<asp:BoundField DataField="SoftwareVersion" HeaderText="Software Version" SortExpression="[SoftwareVersion]"  />
				<asp:BoundField DataField="Origin" HeaderText="Origin" SortExpression="[Origin]"  />
				<asp:BoundField DataField="Size" HeaderText="Size" SortExpression="[Size]"  />
				<asp:BoundField DataField="Quadkey18" HeaderText="Quadkey18" SortExpression="[Quadkey18]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Media Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnMedia" OnClientClick="javascript:location.href='MediaEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:MediaDataSource ID="MediaDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
		>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:MediaDataSource>
	    		
</asp:Content>



