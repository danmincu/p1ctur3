<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="MediaAudio.aspx.cs" Inherits="MediaAudio" Title="MediaAudio List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Media Audio List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="MediaAudioDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_MediaAudio.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="AudioChannels" HeaderText="Audio Channels" SortExpression="[AudioChannels]"  />
				<asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="[Duration]"  />
				<asp:BoundField DataField="AudioCodec" HeaderText="Audio Codec" SortExpression="[AudioCodec]"  />
				<data:HyperLinkField HeaderText="Id" DataNavigateUrlFormatString="MediaEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="IdSource" DataTextField="CaptureDateTime" />
			</Columns>
			<EmptyDataTemplate>
				<b>No MediaAudio Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnMediaAudio" OnClientClick="javascript:location.href='MediaAudioEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:MediaAudioDataSource ID="MediaAudioDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:MediaAudioProperty Name="Media"/> 
					<%--<data:MediaAudioProperty Name="MediaVideo" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:MediaAudioDataSource>
	    		
</asp:Content>



