package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class RegisterBasicRequest {

    @SerializedName("CityId")
    @Expose
    private String CityId;

    @SerializedName("Mobile")
    @Expose
    private String Mobile;

    @SerializedName("Username")
    @Expose
    private String Username;

    @SerializedName("Password")
    @Expose
    private String Password;

    @SerializedName("FullName")
    @Expose
    private String FullName;

    @SerializedName("Email")
    @Expose
    private String Email;

    @SerializedName("DOP")
    @Expose
    private String DOP;

    @SerializedName("Flag")
    @Expose
    private String Flag;

    @SerializedName("IdentifierId")
    @Expose
    private String IdentifierId;

    @SerializedName("LicenceId")
    @Expose
    private String LicenceId;

    @SerializedName("Identifier")
    @Expose
    private Content Identifier;

    @SerializedName("Licence")
    @Expose
    private Content Licence;
    @SerializedName("NotificationType")
    @Expose
    private String NotificationType;

    @SerializedName("Gender")
    @Expose
    private String Gender;

    public String getCityId() {
        return CityId;
    }

    public void setCityId(String cityId) {
        CityId = cityId;
    }

    public String getMobile() {
        return Mobile;
    }

    public void setMobile(String mobile) {
        Mobile = mobile;
    }

    public String getUsername() {
        return Username;
    }

    public void setUsername(String username) {
        Username = username;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public String getDOP() {
        return DOP;
    }

    public void setDOP(String DOP) {
        this.DOP = DOP;
    }

    public String getFlag() {
        return Flag;
    }

    public void setFlag(String flag) {
        Flag = flag;
    }

    public String getIdentifierId() {
        return IdentifierId;
    }

    public void setIdentifierId(String identifierId) {
        IdentifierId = identifierId;
    }

    public String getLicenceId() {
        return LicenceId;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }


    public void setLicenceId(String licenceId) {
        LicenceId = licenceId;
    }

    public Content getIdentifier() {
        return Identifier;
    }

    public void setIdentifier(Content identifier) {
        Identifier = identifier;
    }

    public Content getLicence() {
        return Licence;
    }

    public void setLicence(Content licence) {
        Licence = licence;
    }

    public String getNotificationType() {
        return NotificationType;
    }

    public void setNotificationType(String notificationType) {
        NotificationType = notificationType;
    }

    public String getGender() {
        return Gender;
    }

    public void setGender(String gender) {
        Gender = gender;
    }


    public String getFullName() {
        return FullName;
    }

    public void setFullName(String fullName) {
        FullName = fullName;
    }

}
