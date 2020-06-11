//
//  RegisterRequest.swift
//  ERent
//
//  Created by Anas Alhasani on 5/26/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct RegisterRequest {
    let fullname : String
    let username: String
    let email: String
    let phoneNumber: String
    let password: String
    let identifierId: Int
    let licenseId: Int
    let birthdate: String
    let country: String
    let city: Int
    let licensePhoto: Data
    let profilePhoto: Data
    let gender : Int
}



extension RegisterRequest: APIRequest {
    
    var parameters: JSONDictionary {
        let licencePhotoDict = NSMutableDictionary()
        licencePhotoDict.setValue(licensePhoto.base64EncodedString(), forKey: "Content")
        licencePhotoDict.setValue(username+"_"+"\(identifierId)"+".jpg", forKey: "FileName")
        
        let profilePhotoDict = NSMutableDictionary()
        profilePhotoDict.setValue(licensePhoto.base64EncodedString(), forKey: "Content")
        profilePhotoDict.setValue(username+"_"+"\(identifierId)"+".jpg", forKey: "FileName")
        
        let dict = NSMutableDictionary()
        dict.setValue(fullname, forKey: "FullName")
        dict.setValue(city, forKey: "CityId")
        dict.setValue(phoneNumber, forKey: "Mobile")
        dict.setValue(email, forKey: "Email")
        dict.setValue(username, forKey: "Username")
        dict.setValue(password, forKey: "Password")
        dict.setValue(birthdate, forKey: "DOP")
        dict.setValue(0, forKey: "Flag")
        dict.setValue(identifierId, forKey: "IdentifierId")
        dict.setValue(licenseId, forKey: "LicenceId")
        dict.setValue(profilePhotoDict, forKey: "Identifier")
        dict.setValue(licencePhotoDict, forKey: "Licence")
        dict.setValue(gender, forKey: "Gender")
        
        let data = NSMutableDictionary()
        data.setValue(dict, forKey: "Data")
        
        return data as! JSONDictionary

    }
    
    var resourceName: String {
        return "Account/SignUp"
    }

        func makeModel(from json: JSONDictionary) -> Void { }
}

