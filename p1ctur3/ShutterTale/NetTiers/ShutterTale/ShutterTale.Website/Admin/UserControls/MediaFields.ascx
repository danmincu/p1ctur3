<%@ Control Language="C#" ClassName="MediaFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataId" runat="server" Text="Id:" AssociatedControlID="dataId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataId" Value='<%# Bind("Id") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCaptureDateTime" runat="server" Text="Capture Date Time:" AssociatedControlID="dataCaptureDateTime" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCaptureDateTime" Text='<%# Bind("CaptureDateTime", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataCaptureDateTime" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFileDateTime" runat="server" Text="File Date Time:" AssociatedControlID="dataFileDateTime" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFileDateTime" Text='<%# Bind("FileDateTime", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFileDateTime" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" /><asp:RequiredFieldValidator ID="ReqVal_dataFileDateTime" runat="server" Display="Dynamic" ControlToValidate="dataFileDateTime" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataImportDateTime" runat="server" Text="Import Date Time:" AssociatedControlID="dataImportDateTime" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataImportDateTime" Text='<%# Bind("ImportDateTime", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataImportDateTime" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" /><asp:RequiredFieldValidator ID="ReqVal_dataImportDateTime" runat="server" Display="Dynamic" ControlToValidate="dataImportDateTime" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataLocation" runat="server" Text="Location:" AssociatedControlID="dataLocation" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataLocation" Value='<%# Bind("Location") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPixelx" runat="server" Text="Pixelx:" AssociatedControlID="dataPixelx" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPixelx" Text='<%# Bind("Pixelx") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataPixelx" runat="server" Display="Dynamic" ControlToValidate="dataPixelx" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataPixelx" runat="server" Display="Dynamic" ControlToValidate="dataPixelx" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPixely" runat="server" Text="Pixely:" AssociatedControlID="dataPixely" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPixely" Text='<%# Bind("Pixely") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataPixely" runat="server" Display="Dynamic" ControlToValidate="dataPixely" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataPixely" runat="server" Display="Dynamic" ControlToValidate="dataPixely" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDpi" runat="server" Text="Dpi:" AssociatedControlID="dataDpi" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDpi" Text='<%# Bind("Dpi") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataDpi" runat="server" Display="Dynamic" ControlToValidate="dataDpi" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataDpi" runat="server" Display="Dynamic" ControlToValidate="dataDpi" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataContentType" runat="server" Text="Content Type:" AssociatedControlID="dataContentType" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataContentType" Text='<%# Bind("ContentType") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataContentType" runat="server" Display="Dynamic" ControlToValidate="dataContentType" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMake" runat="server" Text="Make:" AssociatedControlID="dataMake" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMake" Text='<%# Bind("Make") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataModel" runat="server" Text="Model:" AssociatedControlID="dataModel" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataModel" Text='<%# Bind("Model") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataSoftwareVersion" runat="server" Text="Software Version:" AssociatedControlID="dataSoftwareVersion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataSoftwareVersion" Text='<%# Bind("SoftwareVersion") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataOrigin" runat="server" Text="Origin:" AssociatedControlID="dataOrigin" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataOrigin" Text='<%# Bind("Origin") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataOrigin" runat="server" Display="Dynamic" ControlToValidate="dataOrigin" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataSize" runat="server" Text="Size:" AssociatedControlID="dataSize" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataSize" Text='<%# Bind("Size") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataSize" runat="server" Display="Dynamic" ControlToValidate="dataSize" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataSize" runat="server" Display="Dynamic" ControlToValidate="dataSize" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataQuadkey18" runat="server" Text="Quadkey18:" AssociatedControlID="dataQuadkey18" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataQuadkey18" Text='<%# Bind("Quadkey18") %>' MaxLength="18"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataQuadkey18" runat="server" Display="Dynamic" ControlToValidate="dataQuadkey18" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


