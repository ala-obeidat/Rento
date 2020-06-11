//
//  ListRequestService.swift
//  ERent
//
//  Created by Anas Alhasani on 5/26/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct ListRequestService: APIRequest {
    
    let isHistory: Bool
    
    var resourceName: String {
        return "Car/ListRequest"
    }
    
    var parameters: JSONDictionary {
        return ["Data": isHistory]
    }
    
    func makeModel(from json: JSONDictionary) -> [CarModel] {
        var cars: [CarModel] = []
        
        //FIXME: - Double check CarsArray Key
        let jsonDictionaryArray = json["Data"] as? [JSONDictionary] ?? []
        
        for dictionary in jsonDictionaryArray {
            let car = CarModel()
            car.model = "\(dictionary["Model"] as? Int ?? 0)"
            car.subTypeNameAr = dictionary["SubTypeNameAr"] as? String ?? "-"
            car.id = "\(dictionary["Id"] as? Int ?? 0)"
            car.subTypeNameEn = dictionary["SubTypeNameEn"] as? String ?? "-"
            car.subType = "\(dictionary["CarSubType"] as? Int ?? 0)"
            let action = dictionary["Action"] as? Int ?? 0
            let actionType = CarModel.Action(rawValue: action)
            car.actionType = actionType
//            car.date  = dictionary["CreateDate"] as? String ?? "-"
//
            let str = dictionary["CreateDate"] as? String ?? "-"
            let substring = str[..<10]
            if str.count >= 10 {
                let string = String(substring)
                car.date = string
                
            }else{
                car.date = str
                
            }
            
            
            let start = str.index(str.startIndex, offsetBy: 11)
            let end = str.index(str.endIndex, offsetBy: -3)
            let range = start..<end
            
            
            car.time = str.substring(with: range)
            
            
            car.typeNameAr = dictionary["TypeNameAr"] as? String ?? "-"
            car.type = "\(dictionary["CarType"] as? Int ?? 0)"
            car.officeName = dictionary["OfficeName"] as? String ?? "-"
            car.typeNameEn = dictionary["TypeNameEn"] as? String ?? "-"
            car.price = "\(dictionary["Price"] as? Int ?? 0)"
     
            
            car.dayCost = "\(dictionary["DayCost"] as? Int ?? 0)"
            
            
            let imgStr = "http://erent.ashhalan.com/assets/images/cars/" + car.typeNameEn + "/" + car.subTypeNameEn + ".png"
            car.image = imgStr
            cars.append(car)
        }
        
        return cars
    }
}
