//
//  ListOfferRequest.swift
//  ERent
//
//  Created by Anas Alhasani on 5/26/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct ListOfferRequest: APIRequest {
    
    var resourceName: String {
        return "Offer/List"
    }
    
    var parameters: JSONDictionary {
        return [:]
    }
    
    func makeModel(from json: JSONDictionary) -> [CarModel] {
        var cars: [CarModel] = []
        
        let jsonDictionaryArray = json["Data"] as? [JSONDictionary] ?? []
        
        for dictionary in jsonDictionaryArray {
            let car = CarModel()
            
            car.model = "\(dictionary["CarModel"] as? Int ?? 0)"
            car.type = "\(dictionary["CarType"] as? Int ?? 0)"
            car.subType = "\(dictionary["CarSubType"] as? Int ?? 0)"
            
            let str = dictionary["CreateDate"] as? String ?? "-"
            let substring = str[..<10]
            if str.count >= 10 {
                let string = String(substring)
                car.date = string
            }else{
                car.date = str
            }
            
            
            
            let fromStr = dictionary["From"] as? String ?? "-"
            let fromSubstring = fromStr[..<10]
            if fromStr.count >= 10 {
                let string = String(fromSubstring)
                car.date = string
            }else{
                car.date = fromStr
            }
            
            let toStr = dictionary["From"] as? String ?? "-"
            let toSubstring = toStr[..<10]
            if toSubstring.count >= 10 {
                let string = String(toSubstring)
                car.date = string
            }else{
                car.date = toStr
            }
            car.cost = "\(dictionary["Cost"] as? Int ?? 0)"
            
            car.typeNameAr = dictionary["TypeNameAr"] as? String ?? "-"
            car.typeNameEn = dictionary["TypeNameEn"] as? String ?? "-"
            
            car.subTypeNameAr = dictionary["SubTypeNameAr"] as? String ?? "-"
            car.subTypeNameEn = dictionary["SubTypeNameEn"] as? String ?? "-"
            
            car.providerName = dictionary["ProviderName"] as? String ?? "-"
            car.discount = "\(dictionary["Discount"] as? Int ?? 0)"
            
            car.id = "\(dictionary["Id"] as? Int ?? 0)"
            
            car.period = "\(dictionary["Period"] as? Int ?? 0)"
            
         
            
         let imgStr = "http://erent.ashhalan.com/assets/images/cars/" + car.typeNameEn + "/" + car.subTypeNameEn + ".png"
            car.image = imgStr
            // + car.typeNameEn + car.subTypeNameEn
            
            cars.append(car)
        }
        
        return cars
    }
}
