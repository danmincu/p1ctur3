<%@ Control Language="C#" ClassName="MediaImageFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataOrientation" runat="server" Text="Orientation:" AssociatedControlID="dataOrientation" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataOrientation" Text='<%# Bind("Orientation") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataOrientation" runat="server" Display="Dynamic" ControlToValidate="dataOrientation" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataYcbCrPositioning" runat="server" Text="Ycb Cr Positioning:" AssociatedControlID="dataYcbCrPositioning" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataYcbCrPositioning" Text='<%# Bind("YcbCrPositioning") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataYcbCrPositioning" runat="server" Display="Dynamic" ControlToValidate="dataYcbCrPositioning" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataExposureTime" runat="server" Text="Exposure Time:" AssociatedControlID="dataExposureTime" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataExposureTime" Text='<%# Bind("ExposureTime") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataExposureTime" runat="server" Display="Dynamic" ControlToValidate="dataExposureTime" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFnumber" runat="server" Text="Fnumber:" AssociatedControlID="dataFnumber" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFnumber" Text='<%# Bind("Fnumber") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataFnumber" runat="server" Display="Dynamic" ControlToValidate="dataFnumber" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataExposureProgram" runat="server" Text="Exposure Program:" AssociatedControlID="dataExposureProgram" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataExposureProgram" Text='<%# Bind("ExposureProgram") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataExposureProgram" runat="server" Display="Dynamic" ControlToValidate="dataExposureProgram" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIsoSpeedRatings" runat="server" Text="Iso Speed Ratings:" AssociatedControlID="dataIsoSpeedRatings" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataIsoSpeedRatings" Text='<%# Bind("IsoSpeedRatings") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataIsoSpeedRatings" runat="server" Display="Dynamic" ControlToValidate="dataIsoSpeedRatings" ErrorMessage="Invalid value" MaximumValue="32767" MinimumValue="-32768" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataShutterSpeedValue" runat="server" Text="Shutter Speed Value:" AssociatedControlID="dataShutterSpeedValue" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataShutterSpeedValue" Text='<%# Bind("ShutterSpeedValue") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataShutterSpeedValue" runat="server" Display="Dynamic" ControlToValidate="dataShutterSpeedValue" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataApertureValue" runat="server" Text="Aperture Value:" AssociatedControlID="dataApertureValue" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataApertureValue" Text='<%# Bind("ApertureValue") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataApertureValue" runat="server" Display="Dynamic" ControlToValidate="dataApertureValue" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMeteringMode" runat="server" Text="Metering Mode:" AssociatedControlID="dataMeteringMode" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMeteringMode" Text='<%# Bind("MeteringMode") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataMeteringMode" runat="server" Display="Dynamic" ControlToValidate="dataMeteringMode" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFlash" runat="server" Text="Flash:" AssociatedControlID="dataFlash" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFlash" Text='<%# Bind("Flash") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataFlash" runat="server" Display="Dynamic" ControlToValidate="dataFlash" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFocalLength" runat="server" Text="Focal Length:" AssociatedControlID="dataFocalLength" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFocalLength" Text='<%# Bind("FocalLength") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataFocalLength" runat="server" Display="Dynamic" ControlToValidate="dataFocalLength" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFlashpixVersion" runat="server" Text="Flashpix Version:" AssociatedControlID="dataFlashpixVersion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFlashpixVersion" Text='<%# Bind("FlashpixVersion") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataColorSpace" runat="server" Text="Color Space:" AssociatedControlID="dataColorSpace" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataColorSpace" Text='<%# Bind("ColorSpace") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataColorSpace" runat="server" Display="Dynamic" ControlToValidate="dataColorSpace" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataSensingMethod" runat="server" Text="Sensing Method:" AssociatedControlID="dataSensingMethod" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataSensingMethod" Text='<%# Bind("SensingMethod") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataSensingMethod" runat="server" Display="Dynamic" ControlToValidate="dataSensingMethod" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataExposureMode" runat="server" Text="Exposure Mode:" AssociatedControlID="dataExposureMode" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataExposureMode" Text='<%# Bind("ExposureMode") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataExposureMode" runat="server" Display="Dynamic" ControlToValidate="dataExposureMode" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataWhiteBalance" runat="server" Text="White Balance:" AssociatedControlID="dataWhiteBalance" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataWhiteBalance" Text='<%# Bind("WhiteBalance") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataWhiteBalance" runat="server" Display="Dynamic" ControlToValidate="dataWhiteBalance" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataSceneCaptureType" runat="server" Text="Scene Capture Type:" AssociatedControlID="dataSceneCaptureType" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataSceneCaptureType" Text='<%# Bind("SceneCaptureType") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataSceneCaptureType" runat="server" Display="Dynamic" ControlToValidate="dataSceneCaptureType" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataSharpness" runat="server" Text="Sharpness:" AssociatedControlID="dataSharpness" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataSharpness" Text='<%# Bind("Sharpness") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataSharpness" runat="server" Display="Dynamic" ControlToValidate="dataSharpness" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataId" runat="server" Text="Id:" AssociatedControlID="dataId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataId" DataSourceID="IdMediaDataSource" DataTextField="CaptureDateTime" DataValueField="Id" SelectedValue='<%# Bind("Id") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:MediaDataSource ID="IdMediaDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


