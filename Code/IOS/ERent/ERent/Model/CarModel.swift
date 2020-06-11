//
//  carModel.swift
//  ERent
//
//  Created by Rexxer on 4/29/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class CarModel: NSObject ,NSCoding {
    
    enum Action: Int {
        case pending
        case processing
        case approved
        case onTheWay
        case delivered
        case done
        case rejected
    }
    
    var image : String? = "https://www.autotrader.co.uk/images/at3/sell/landing-pages/hero-car.png"
    var modelName : String? = nil
    var type : String? = nil
    var model : String? = nil
    var year : String? = nil
    var officeFlag : String? = nil
    var color : String? = nil
    var colorName : String? = nil
    var adress : String? = nil
    
    var isFavorite : Bool = false
    var mobile : String? = nil
    
    var date : String? = nil
    var time : String? = nil
    var status : String? = nil
    
    var offerDate : String? = nil
    var price : String? = nil
    var discount : String? = nil
    var longitude : String? = nil
    var latitude : String? = nil
    var rectingOffice : String? = nil
    
    var subType = ""
    var actionType: Action?
    var officeName = ""
    var id = ""
    
    var subTypeNameAr = ""
    var subTypeNameEn = ""
    var typeNameEn = ""
    var typeNameAr = ""
    
    var dayCost = ""
    var cost = ""
    
    var providerName = ""
    var period = ""
    //////
    
    
    var cityId = 0
    var additionalKiloCost = 0
    var countryId = 0
    var desc: String = ""
    var flag = 0
    var kiloLimit = 0
    var kiloNumber = 0
    var monthCost = 0
    var weekCost = 0
    
    var imageIds = [String]()
    
    var sellerName : String? = nil
    var sellerMobile : String? = nil
    
    
    required convenience init(coder aDecoder: NSCoder) {
        
        self.init()
        image = aDecoder.decodeObject(forKey: "image") as? String
        modelName = aDecoder.decodeObject(forKey: "modelName") as? String
        type = aDecoder.decodeObject(forKey: "type") as? String
        model = aDecoder.decodeObject(forKey: "model") as? String
        year = aDecoder.decodeObject(forKey: "year") as? String
        color = aDecoder.decodeObject(forKey: "color") as? String
        colorName = aDecoder.decodeObject(forKey: "colorName") as? String
        adress = aDecoder.decodeObject(forKey: "adress") as? String
        isFavorite = aDecoder.decodeBool(forKey: "isFavorite")
     
        mobile = aDecoder.decodeObject(forKey: "mobile") as? String
        date = aDecoder.decodeObject(forKey: "date") as? String
        time = aDecoder.decodeObject(forKey: "time") as? String
        status = aDecoder.decodeObject(forKey: "status") as? String
        offerDate = aDecoder.decodeObject(forKey: "offerDate") as? String
        price = aDecoder.decodeObject(forKey: "price") as? String
        discount = aDecoder.decodeObject(forKey: "discount") as? String
        rectingOffice = aDecoder.decodeObject(forKey: "rectingOffice") as? String
        subType = aDecoder.decodeObject(forKey: "subType") as! String
//        self.actionType = aDecoder.decodeObject(forKey: "actionType")
        actionType = CarModel.Action(rawValue: Int(aDecoder.decodeCInt(forKey: "actionType")))
        officeName = aDecoder.decodeObject(forKey: "officeName") as! String
        id = aDecoder.decodeObject(forKey: "id") as! String
        subTypeNameAr = aDecoder.decodeObject(forKey: "subTypeNameAr") as! String
        subTypeNameEn = aDecoder.decodeObject(forKey: "subTypeNameEn") as! String
        typeNameEn = aDecoder.decodeObject(forKey: "typeNameEn") as! String
        typeNameAr = aDecoder.decodeObject(forKey: "typeNameAr") as! String
        dayCost = aDecoder.decodeObject(forKey: "dayCost") as! String
        cost = aDecoder.decodeObject(forKey: "cost") as! String
        providerName = aDecoder.decodeObject(forKey: "providerName") as! String
        period = aDecoder.decodeObject(forKey: "period") as! String
        
        sellerName = aDecoder.decodeObject(forKey: "OfficeName") as? String
        sellerMobile = aDecoder.decodeObject(forKey: "OfficeMobile") as? String
          latitude = aDecoder.decodeObject(forKey: "Latitude") as? String
          longitude = aDecoder.decodeObject(forKey: "Longitude") as? String
        officeFlag = aDecoder.decodeObject(forKey: "OfficeFlag") as? String
    }
   
    
    func encode(with aCoder: NSCoder) {
        
        aCoder.encode(image, forKey: "image")
        aCoder.encode(modelName , forKey: "modelName")
        aCoder.encode(modelName , forKey: "modelName")
        aCoder.encode(type , forKey: "type")
        aCoder.encode(model,forKey:"model")
        aCoder.encode(year,forKey:"year")
        aCoder.encode(color,forKey:"color")
        aCoder.encode(colorName,forKey:"colorName")
        aCoder.encode(adress,forKey:"adress")
        aCoder.encode(isFavorite,forKey:"isFavorite")
        aCoder.encode(mobile,forKey:"mobile")
        aCoder.encode(date,forKey:"date")
        aCoder.encode(time,forKey:"time")
        aCoder.encode(status,forKey:"status")
        aCoder.encode(offerDate,forKey:"offerDate")
        aCoder.encode(price,forKey:"price")
        aCoder.encode(discount,forKey:"discount")
        aCoder.encode(rectingOffice,forKey:"rectingOffice")
        aCoder.encode(subType,forKey:"subType")
        aCoder.encode(actionType?.rawValue ?? 0,forKey:"actionType")
        aCoder.encode(officeName,forKey:"officeName")
        aCoder.encode(id,forKey:"id")
        aCoder.encode(subTypeNameAr,forKey:"subTypeNameAr")
        aCoder.encode(subTypeNameEn,forKey:"subTypeNameEn")
        aCoder.encode(typeNameEn,forKey:"typeNameEn")
        aCoder.encode(typeNameAr,forKey:"typeNameAr")
        aCoder.encode(dayCost,forKey:"dayCost")
        aCoder.encode(cost,forKey:"cost")
        aCoder.encode(providerName,forKey:"providerName")
        aCoder.encode(period,forKey:"period")
        
          aCoder.encode(sellerName, forKey: "OfficeName")
          aCoder.encode(sellerMobile, forKey: "OfficeMobile")
        aCoder.encode(latitude, forKey: "Latitude")
        aCoder.encode(longitude, forKey: "Longitude")
         aCoder.encode(officeFlag, forKey: "OfficeFlag")
        
        
    }
    
    
    func getTypeName() -> String {
        let lang = UserDefaults.standard.object(forKey: "language") as? String
        if lang == "ar" {
            return typeNameAr
        }
            return typeNameEn
        
    }
    func getSubTypeName() -> String {
        let lang = UserDefaults.standard.object(forKey: "language") as? String
        if lang == "ar" {
            return subTypeNameAr
        }
        return subTypeNameEn
        
    }
    
}
