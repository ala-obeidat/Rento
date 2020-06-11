package com.epicloudtech.erent.models.requests;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class FormRequest {
    @SerializedName("Subject")
    @Expose
    private String Subject;

    @SerializedName("Body")
    @Expose
    private String Body;

    @SerializedName("Email")
    @Expose
    private String Email;

    @SerializedName("Mobile")
    @Expose
    private String Mobile;
    @SerializedName("Name")
    @Expose
    private String Name;

    public String getSubject() {
        return Subject;
    }

    public void setSubject(String subject) {
        Subject = subject;
    }

    public String getBody() {
        return Body;
    }

    public void setBody(String body) {
        Body = body;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public String getMobile() {
        return Mobile;
    }

    public void setMobile(String mobile) {
        Mobile = mobile;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

}
