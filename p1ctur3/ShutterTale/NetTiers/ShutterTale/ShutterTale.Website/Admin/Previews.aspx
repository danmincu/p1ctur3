<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Previews.aspx.cs" Inherits="Previews" Title="Previews List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Previews List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PreviewsDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Previews.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="[Id]" ReadOnly="True" />
				<asp:BoundField DataField="Level0" HeaderText="Level0" SortExpression="[Level0]"  />
				<asp:BoundField DataField="Level1" HeaderText="Level1" SortExpression="[Level1]"  />
				<asp:BoundField DataField="Level2" HeaderText="Level2" SortExpression="[Level2]"  />
				<asp:BoundField DataField="Level3" HeaderText="Level3" SortExpression="[Level3]"  />
				<asp:BoundField DataField="PreviewType" HeaderText="Preview Type" SortExpression="[PreviewType]"  />
				<data:HyperLinkField HeaderText="Medium Id" DataNavigateUrlFormatString="MediaEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="MediumIdSource" DataTextField="CaptureDateTime" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Previews Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPreviews" OnClientClick="javascript:location.href='PreviewsEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PreviewsDataSource ID="PreviewsDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PreviewsProperty Name="Media"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:PreviewsDataSource>
	    		
</asp:Content>



