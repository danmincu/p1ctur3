<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="MediaEdit.aspx.cs" Inherits="MediaEdit" Title="Media Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Media - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" DataSourceID="MediaDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/MediaFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/MediaFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Media not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:MediaDataSource ID="MediaDataSource" runat="server"
			SelectMethod="GetById"
		>
			<Parameters>
				<asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />

			</Parameters>
		</data:MediaDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewPreviews1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPreviews1_SelectedIndexChanged"			 			 
			DataSourceID="PreviewsDataSource1"
			DataKeyNames="Id"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Previews.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Level0" HeaderText="Level0" SortExpression="[Level0]" />				
				<asp:BoundField DataField="Level1" HeaderText="Level1" SortExpression="[Level1]" />				
				<asp:BoundField DataField="Level2" HeaderText="Level2" SortExpression="[Level2]" />				
				<asp:BoundField DataField="Level3" HeaderText="Level3" SortExpression="[Level3]" />				
				<asp:BoundField DataField="PreviewType" HeaderText="Preview Type" SortExpression="[PreviewType]" />				
				<data:HyperLinkField HeaderText="Medium Id" DataNavigateUrlFormatString="MediaEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="MediumIdSource" DataTextField="CaptureDateTime" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Previews Found! </b>
				<asp:HyperLink runat="server" ID="hypPreviews" NavigateUrl="~/admin/PreviewsEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PreviewsDataSource ID="PreviewsDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PreviewsProperty Name="Media"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PreviewsFilter  Column="MediumId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PreviewsDataSource>		
		
		<br />
		

</asp:Content>

