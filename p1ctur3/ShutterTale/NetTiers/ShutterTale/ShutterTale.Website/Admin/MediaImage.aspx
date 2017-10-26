<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="MediaImage.aspx.cs" Inherits="MediaImage" Title="MediaImage List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Media Image List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="MediaImageDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_MediaImage.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="Orientation" HeaderText="Orientation" SortExpression="[Orientation]"  />
				<asp:BoundField DataField="YcbCrPositioning" HeaderText="Ycb Cr Positioning" SortExpression="[YCbCrPositioning]"  />
				<asp:BoundField DataField="ExposureTime" HeaderText="Exposure Time" SortExpression="[ExposureTime]"  />
				<asp:BoundField DataField="Fnumber" HeaderText="Fnumber" SortExpression="[FNumber]"  />
				<asp:BoundField DataField="ExposureProgram" HeaderText="Exposure Program" SortExpression="[ExposureProgram]"  />
				<asp:BoundField DataField="IsoSpeedRatings" HeaderText="Iso Speed Ratings" SortExpression="[ISOSpeedRatings]"  />
				<asp:BoundField DataField="ShutterSpeedValue" HeaderText="Shutter Speed Value" SortExpression="[ShutterSpeedValue]"  />
				<asp:BoundField DataField="ApertureValue" HeaderText="Aperture Value" SortExpression="[ApertureValue]"  />
				<asp:BoundField DataField="MeteringMode" HeaderText="Metering Mode" SortExpression="[MeteringMode]"  />
				<asp:BoundField DataField="Flash" HeaderText="Flash" SortExpression="[Flash]"  />
				<asp:BoundField DataField="FocalLength" HeaderText="Focal Length" SortExpression="[FocalLength]"  />
				<asp:BoundField DataField="FlashpixVersion" HeaderText="Flashpix Version" SortExpression="[FlashpixVersion]"  />
				<asp:BoundField DataField="ColorSpace" HeaderText="Color Space" SortExpression="[ColorSpace]"  />
				<asp:BoundField DataField="SensingMethod" HeaderText="Sensing Method" SortExpression="[SensingMethod]"  />
				<asp:BoundField DataField="ExposureMode" HeaderText="Exposure Mode" SortExpression="[ExposureMode]"  />
				<asp:BoundField DataField="WhiteBalance" HeaderText="White Balance" SortExpression="[WhiteBalance]"  />
				<asp:BoundField DataField="SceneCaptureType" HeaderText="Scene Capture Type" SortExpression="[SceneCaptureType]"  />
				<asp:BoundField DataField="Sharpness" HeaderText="Sharpness" SortExpression="[Sharpness]"  />
				<data:HyperLinkField HeaderText="Id" DataNavigateUrlFormatString="MediaEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="IdSource" DataTextField="CaptureDateTime" />
			</Columns>
			<EmptyDataTemplate>
				<b>No MediaImage Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnMediaImage" OnClientClick="javascript:location.href='MediaImageEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:MediaImageDataSource ID="MediaImageDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:MediaImageProperty Name="Media"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:MediaImageDataSource>
	    		
</asp:Content>



