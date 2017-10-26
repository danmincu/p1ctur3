<%@ Control Language="C#" ClassName="MediaAudioFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataAudioChannels" runat="server" Text="Audio Channels:" AssociatedControlID="dataAudioChannels" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataAudioChannels" Text='<%# Bind("AudioChannels") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataAudioChannels" runat="server" Display="Dynamic" ControlToValidate="dataAudioChannels" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataAudioChannels" runat="server" Display="Dynamic" ControlToValidate="dataAudioChannels" ErrorMessage="Invalid value" MaximumValue="255" MinimumValue="0" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDuration" runat="server" Text="Duration:" AssociatedControlID="dataDuration" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDuration" Text='<%# Bind("Duration") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataDuration" runat="server" Display="Dynamic" ControlToValidate="dataDuration" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataAudioCodec" runat="server" Text="Audio Codec:" AssociatedControlID="dataAudioCodec" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataAudioCodec" Text='<%# Bind("AudioCodec") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataAudioCodec" runat="server" Display="Dynamic" ControlToValidate="dataAudioCodec" ErrorMessage="Required"></asp:RequiredFieldValidator>
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


