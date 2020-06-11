//
//  CheckOutRequest.swift
//  ERent
//
//  Created by Rexxer on 6/3/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct CheckOutRequest {

    let carId : Int
    let cityId : Int
    let price : Int
    let from : String
    let to : String
    let flag : Int
    let longitude : Double
    let latitude : Double
    
}



extension CheckOutRequest: APIRequest {
    
    var parameters: JSONDictionary {
        
        //dict.setValue(birthdate.toString(.registrationBirthdate), forKey: "DOP")
        //        let licencePhotoDict = NSMutableDictionary()
        //        licencePhotoDict.setValue(licensePhoto.base64EncodedString(), forKey: "Content")
        //        licencePhotoDict.setValue(username+"_"+"\(identifierId)"+".jpg", forKey: "FileName")
        //
        //        let profilePhotoDict = NSMutableDictionary()
        //        profilePhotoDict.setValue(licensePhoto.base64EncodedString(), forKey: "Content")
        //        profilePhotoDict.setValue(username+"_"+"\(identifierId)"+".jpg", forKey: "FileName")
        //
//                let dict = NSMutableDictionary()
//                dict.setValue(city, forKey: "CityId")
//                dict.setValue(phoneNumber, forKey: "Mobile")
//                dict.setValue(username, forKey: "Username")
//                dict.setValue(password, forKey: "Password")
//                dict.setValue(birthdate.toString(.registrationBirthdate), forKey: "DOP")
//                dict.setValue(0, forKey: "Flag")
//                dict.setValue(identifierId, forKey: "IdentifierId")
//                dict.setValue(licenseId, forKey: "LicenceId")
//                dict.setValue(profilePhotoDict, forKey: "Identifier")
//                dict.setValue(licencePhotoDict, forKey: "Licence")
//
//                let data = NSMutableDictionary()
//                data.setValue(dict, forKey: "Data")
        //
        
        //        return data as! JSONDictionary
        
        if (flag == 0) {
            return [
                "Data": ["CarId": String(carId),
                         "Price" : String(price),
                         "From" : from,
                         "To" : to,
                         "Flag" : String(flag),
                         "CityId" : String(cityId)
                ]]
        }else {
            
            let dict = NSMutableDictionary()
            dict.setValue(String(carId), forKey: "CarId")
            dict.setValue(String(price), forKey: "Price")
            dict.setValue(from, forKey: "From")
            dict.setValue(to, forKey: "To")
            
            dict.setValue(String(flag), forKey: "Flag")
            dict.setValue(String(cityId), forKey: "CityId")
            let location = NSMutableDictionary()
            
            location.setValue(String(longitude), forKey: "Longitude")
            location.setValue(String(latitude), forKey: "Latitude")
            
            dict.setValue(location, forKey: "Location")
            
            let data = NSMutableDictionary()
            data.setValue(dict, forKey: "Data")
            return data as! JSONDictionary
//            return [
//                "Data": ["CarId": String(carId),
//                         "Price" : String(price),
//                         "From" : from,
//                         "To" : to,
//                         "Flag" : String(flag),
//                         "CityId" : String(cityId),
//                         "Location" : ["Longitude" : String(longitude) , "Latitude" : String(latitude) ]
//                ]
//            ]
        }
        
        
    }
    
    var resourceName: String {
        return "Car/Checkout"
    }
    
    func makeModel(from json: JSONDictionary) -> Void { }
}
