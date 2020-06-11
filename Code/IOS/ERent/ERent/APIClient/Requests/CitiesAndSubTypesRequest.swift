//
//  CitiesAndSubTypesRequest.swift
//  ERent
//
//  Created by Rexxer on 5/27/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct CitiesAndSubTypesRequest: APIRequest {
    
    let isCity: Bool
    
    var resourceName: String {
        return "LookUp/ListExternal"
    }
    
    var parameters: JSONDictionary {
        return ["Data": isCity ? "City" : "SubType"]
    }
    
    func makeModel(from json: JSONDictionary) -> [ExternalModel] {
        var items: [ExternalModel] = []
        
        //FIXME: - Double check CarsArray Key
        let jsonDictionaryArray = json["Data"] as? [JSONDictionary] ?? []
        
        for dictionary in jsonDictionaryArray {
            let item = ExternalModel()
            item.id = dictionary["Id"] as? Int ?? 0
            
            item.name =  dictionary["Name"] as? String ?? "-"
            item.nameEn =   dictionary["NameEn"] as? String ?? "-"
            item.externalData =   dictionary["ExternalData"] as? Int ?? 0
            
            items.append(item)
        }
        
        return items
    }
}
