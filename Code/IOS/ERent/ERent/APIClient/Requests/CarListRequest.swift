//
//  CarListRequest.swift
//  ERent
//
//  Created by Anas Alhasani on 5/26/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct CarListService {
    
    enum ServiceType {
        case list
        case search
    }
    
    let serviceType: ServiceType
    let cityId: Int
    let minPrice: Int?
    let maxPrice: Int?
    let isAscending: Bool?
    let isSorted: Bool?
    let carModel: String?
    let type: Int?
    let subType: Int?
    
    init(serviceType: ServiceType,
         cityId: Int,
         minPrice: Int? = nil,
         maxPrice: Int? = nil,
         isAscending: Bool? = nil,
         isSorted: Bool? = nil,
         carModel: String? = nil,
         type: Int? = nil,
         
         subType: Int? = nil) {
        self.serviceType = serviceType
        self.cityId = cityId
        self.maxPrice = maxPrice
        self.minPrice = minPrice
        self.isAscending = isAscending
        self.isSorted = isSorted
        self.carModel = carModel
        self.type = type
        self.subType = subType
    }
}

extension CarListService: APIRequest {
    
    var resourceName: String {
        return "Car/List"
    }
    
    var parameters: JSONDictionary {
        return serviceType == .search ? searchParameters: listParameters
    }
    
    var listParameters: JSONDictionary {
        return  [
            "Data": [
                "CityId": cityId
            ]
        ]
    }
  
    //FIXME: - Double check 
    var searchParameters: JSONDictionary {
        return  [
            "Data": [
                "CityId": cityId,
                "MinPrice": minPrice,
                "MaxPrice": maxPrice,
                "Ascending": isAscending,
                "Sort": isSorted,
                "Model": carModel,
                "Type": type,
                "SubType": subType
            ]
        ]
    }
    
    func makeModel(from json: JSONDictionary) -> [CarModel] {
        
        var cars: [CarModel] = []
        
        //FIXME: - Double check CarsArray Key
        let jsonDictionaryArray = json["Data"] as? [JSONDictionary] ?? []
        
        for dictionary in jsonDictionaryArray {
            let car = CarModel()
            car.model = "\(dictionary["Model"] as? Int ?? 0)"
//            car.subType = dictionary["SubTypeNameAr"] as? String ?? "-"
            car.id = "\(dictionary["Id"] as? Int ?? 0)"
            car.mobile = dictionary["OfficeMobile"] as? String ?? "-"
            car.typeNameEn = dictionary["TypeNameEn"] as? String ?? "-"
            car.subType = "\(dictionary["SubType"] as? Int ?? 0)"
            
            let action = dictionary["Action"] as? Int ?? 0
            let actionType = CarModel.Action(rawValue: action)
            car.actionType = actionType
            
            car.date = dictionary["CreateDate"] as? String ?? "-"
            car.typeNameAr = dictionary["TypeNameAr"] as? String ?? "-"
            car.status = "\(dictionary["Status"] as? Int ?? 0)"
            
            car.color = dictionary["Color"] as? String ?? "-"
            
            car.dayCost = "\(dictionary["DayCost"] as? Int ?? 0)"
            car.type = "\(dictionary["Type"] as? Int ?? 0)"
            
            car.officeName = dictionary["OfficeName"] as? String ?? "-"
            car.subTypeNameEn = dictionary["SubTypeNameEn"] as? String ?? "-"
            car.subTypeNameAr = dictionary["SubTypeNameAr"] as? String ?? "-"
            car.price =  "\(dictionary["Price"] as? Int ?? 0)"
            
            car.sellerName = json["OfficeName"] as? String ?? "-"
            car.sellerMobile = json["OfficeMobile"] as? String ?? "-"
            car.longitude = json["Longitude"] as? String ?? "-"
            car.latitude = json["Latitude"] as? String ?? "-"
            let offlag : Int = json["OfficeFlag"] as? Int ?? 0
            car.officeFlag = String(offlag)
            
            
            
            let imgStr = "http://erent.ashhalan.com/assets/images/cars/" + car.typeNameEn + "/" + car.subTypeNameEn + ".png"
            car.image = imgStr
            
            cars.append(car)
        }
        
        return cars
    }
}
