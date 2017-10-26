<%@ Control Language="C#" ClassName="PreviewsFields" %>

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
        <td class="literal"><asp:Label ID="lbldataLevel0" runat="server" Text="Level0:" AssociatedControlID="dataLevel0" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataLevel0" Value='<%# Bind("Level0") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataLevel1" runat="server" Text="Level1:" AssociatedControlID="dataLevel1" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataLevel1" Value='<%# Bind("Level1") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataLevel2" runat="server" Text="Level2:" AssociatedControlID="dataLevel2" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataLevel2" Value='<%# Bind("Level2") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataLevel3" runat="server" Text="Level3:" AssociatedControlID="dataLevel3" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataLevel3" Value='<%# Bind("Level3") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPreviewType" runat="server" Text="Preview Type:" AssociatedControlID="dataPreviewType" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPreviewType" Text='<%# Bind("PreviewType") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataPreviewType" runat="server" Display="Dynamic" ControlToValidate="dataPreviewType" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMediumId" runat="server" Text="Medium Id:" AssociatedControlID="dataMediumId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataMediumId" DataSourceID="MediumIdMediaDataSource" DataTextField="CaptureDateTime" DataValueField="Id" SelectedValue='<%# Bind("MediumId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:MediaDataSource ID="MediumIdMediaDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


