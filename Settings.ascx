<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Settings.ascx.vb" Inherits="Connect.DNN.Modules.Projects.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<fieldset>
 <div class="dnnFormItem">
  <dnn:label id="plTnWidth" runat="server" controlname="txtTnWidth" suffix=":" />
  <asp:TextBox ID="txtTnWidth" runat="server" Width="60" MaxLength="10" CssClass="NormalTextBox" />&nbsp;
  <asp:CompareValidator ID="Comparevalidator1" runat="server" resourcekey="WholeNr.Error" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtTnWidth" Display="Dynamic" />&nbsp;
  <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" resourcekey="Required.Error" ControlToValidate="txtTnWidth" Display="Dynamic" />
 </div>
 <div class="dnnFormItem">
  <dnn:label id="plTnHeight" runat="server" controlname="txtTnHeight" suffix=":" />
  <asp:TextBox ID="txtTnHeight" runat="server" Width="60" MaxLength="10" CssClass="NormalTextBox" />&nbsp;
  <asp:CompareValidator ID="Comparevalidator2" runat="server" resourcekey="WholeNr.Error" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtTnHeight" Display="Dynamic" />&nbsp;
  <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" resourcekey="Required.Error" ControlToValidate="txtTnHeight" Display="Dynamic" />
 </div>
 <div class="dnnFormItem">
  <dnn:label id="plTnFit" runat="server" controlname="ddTnFit" suffix=":" />
  <asp:DropDownList runat="server" ID="ddTnFit">
   <asp:ListItem Value="Crop" Resourcekey="optCrop" />
   <asp:ListItem Value="Shrink" Resourcekey="optShrink" />
   <asp:ListItem Value="Stretch" Resourcekey="optStretch" />
  </asp:DropDownList>
 </div>
 <div class="dnnFormItem">
  <dnn:label id="plMedWidth" runat="server" controlname="txtMedWidth" suffix=":" />
  <asp:TextBox ID="txtMedWidth" runat="server" Width="60" MaxLength="10" CssClass="NormalTextBox" />&nbsp;
  <asp:CompareValidator ID="Comparevalidator3" runat="server" resourcekey="WholeNr.Error" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtMedWidth" Display="Dynamic" />&nbsp;
  <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" resourcekey="Required.Error" ControlToValidate="txtMedWidth" Display="Dynamic" />
 </div>
 <div class="dnnFormItem">
  <dnn:label id="plMedHeight" runat="server" controlname="txtMedHeight" suffix=":" />
  <asp:TextBox ID="txtMedHeight" runat="server" Width="60" MaxLength="10" CssClass="NormalTextBox" />&nbsp;
  <asp:CompareValidator ID="Comparevalidator4" runat="server" resourcekey="WholeNr.Error" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtMedHeight" Display="Dynamic" />&nbsp;
  <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" resourcekey="Required.Error" ControlToValidate="txtMedHeight" Display="Dynamic" />
 </div>
 <div class="dnnFormItem">
  <dnn:label id="plMedFit" runat="server" controlname="ddMedFit" suffix=":" />
  <asp:DropDownList runat="server" ID="ddMedFit">
   <asp:ListItem Value="Crop" Resourcekey="optCrop" />
   <asp:ListItem Value="Shrink" Resourcekey="optShrink" />
   <asp:ListItem Value="Stretch" Resourcekey="optStretch" />
  </asp:DropDownList>
 </div>
 <div class="dnnFormItem">
  <dnn:label id="plZoomWidth" runat="server" controlname="txtZoomWidth" suffix=":" />
  <asp:TextBox ID="txtZoomWidth" runat="server" Width="60" MaxLength="10" CssClass="NormalTextBox" />&nbsp;
  <asp:CompareValidator ID="Comparevalidator5" runat="server" resourcekey="WholeNr.Error" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtZoomWidth" Display="Dynamic" />&nbsp;
  <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" resourcekey="Required.Error" ControlToValidate="txtZoomWidth" Display="Dynamic" />
 </div>
 <div class="dnnFormItem">
  <dnn:label id="plZoomHeight" runat="server" controlname="txtZoomHeight" suffix=":" />
  <asp:TextBox ID="txtZoomHeight" runat="server" Width="60" MaxLength="10" CssClass="NormalTextBox" />&nbsp;
  <asp:CompareValidator ID="Comparevalidator6" runat="server" resourcekey="WholeNr.Error" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtZoomHeight" Display="Dynamic" />&nbsp;
  <asp:RequiredFieldValidator ID="Requiredfieldvalidator6" runat="server" resourcekey="Required.Error" ControlToValidate="txtZoomHeight" Display="Dynamic" />
 </div>
 <div class="dnnFormItem">
  <dnn:label id="plZoomFit" runat="server" controlname="ddZoomFit" suffix=":" />
  <asp:DropDownList runat="server" ID="ddZoomFit">
   <asp:ListItem Value="Crop" Resourcekey="optCrop" />
   <asp:ListItem Value="Shrink" Resourcekey="optShrink" />
   <asp:ListItem Value="Stretch" Resourcekey="optStretch" />
  </asp:DropDownList>
 </div>
</fieldset>
