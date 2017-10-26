<%@ Control Language="C#" ClassName="MediaVideoFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataVideoChannels" runat="server" Text="Video Channels:" AssociatedControlID="dataVideoChannels" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataVideoChannels" Value='<%# Bind("VideoChannels") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVideoCodec" runat="server" Text="Video Codec:" AssociatedControlID="dataVideoCodec" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataVideoCodec" Text='<%# Bind("VideoCodec") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataVideoCodec" runat="server" Display="Dynamic" ControlToValidate="dataVideoCodec" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataId" runat="server" Text="Id:" AssociatedControlID="dataId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataId" DataSourceID="IdMediaAudioDataSource" DataTextField="AudioChannels" DataValueField="Id" SelectedValue='<%# Bind("Id") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:MediaAudioDataSource ID="IdMediaAudioDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


