//
//  GetCarRequest.swift
//  ERent
//
//  Created by Rexxer on 6/2/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//
//Car/Get
import Foundation

struct GetCarRequest: APIRequest {
    let carId: Int
    
    var resourceName: String {
        return "Car/Get"
    }
    
    var parameters: JSONDictionary {
        return [ "Data": carId]
    }
    
    /*
     
     AdditinalKiloCost = 232;
     CityId = 1;
     CountryId = 0;
     DeletedImages = "<null>";
     Description = "";
     Flag = 0;
     ImageIds = "<null>";
     Images = "<null>";
     KiloLimit = 3322;
     KiloNumber = 252;
     MonthCost = 25151515;
     WeekCost = 3322;
     
     */
    
    func makeModel(from json: JSONDictionary) -> CarModel {
        var car  = CarModel()
        car.model = "\(json["Model"] as? Int ?? 0)"
//        car.subType = json["SubTypeNameAr"] as? String ?? "-"
        car.id = "\(json["Id"] as? Int ?? 0)"
        car.mobile = json["OfficeMobile"] as? String ?? "-"
        car.typeNameEn = json["TypeNameEn"] as? String ?? "-"
       // car.subType = "\(json["CarSubType"] as? Int ?? 0)"
        car.subType = "\(json["SubType"] as? Int ?? 0)"
        
        let action = json["Action"] as? Int ?? 0
        let actionType = CarModel.Action(rawValue: action)
        car.actionType = actionType
        
        car.date = json["CreateDate"] as? String ?? "-"
        car.typeNameAr = json["TypeNameAr"] as? String ?? "-"
        car.status = "\(json["Status"] as? Int ?? 0)"
        
        car.color = json["Color"] as? String ?? "-"
        
        car.dayCost = "\(json["DayCost"] as? Int ?? 0)"
        car.type = "\(json["Type"] as? Int ?? 0)"
        
        car.officeName = json["OfficeName"] as? String ?? "-"
        car.subTypeNameEn = json["SubTypeNameEn"] as? String ?? "-"
        car.subTypeNameAr = json["SubTypeNameAr"] as? String ?? "-"
        car.price =  "\(json["Price"] as? Int ?? 0)"
        
        car.additionalKiloCost = json["AdditinalKiloCost"] as? Int ?? 0
        car.cityId = json["CityId"] as? Int ?? 0
        car.flag = json["Flag"] as? Int ?? 0
        car.kiloLimit = json["KiloLimit"] as? Int ?? 0
        car.kiloNumber = json["KiloNumber"] as? Int ?? 0
        car.monthCost = json["MonthCost"] as? Int ?? 0
        car.weekCost = json["WeekCost"] as? Int ?? 0
        car.sellerName = json["OfficeName"] as? String ?? "-"
        car.sellerMobile = json["OfficeMobile"] as? String ?? "-"
        car.longitude = json["Longitude"] as? String ?? "-"
        car.latitude = json["Latitude"] as? String ?? "-"
        let offlag : Int = json["OfficeFlag"] as? Int ?? 0
        car.officeFlag = String(offlag)
        
        car.imageIds = json["ImageIds"] as? [String] ?? []
        
    let imgStr = "http://erent.ashhalan.com/assets/images/cars/" + car.typeNameEn + "/" + car.subTypeNameEn + ".png"
        car.image = imgStr
        
        
        return car
    }
}
