//
//  TypesAndCountriesRequest.swift
//  ERent
//
//  Created by Rexxer on 5/27/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation


struct TypesAndCountriesRequest: APIRequest {
    
    let isCountry: Bool
    
    var resourceName: String {
        return "LookUp/List"
    }
    
    var parameters: JSONDictionary {
        return ["Data": isCountry ? "Country" : "Type"]
    }
    
    func makeModel(from json: JSONDictionary) -> [BaseModel] {
        var items: [BaseModel] = []
        
        //FIXME: - Double check CarsArray Key
        let jsonDictionaryArray = json["Data"] as? [JSONDictionary] ?? []
        
        for dictionary in jsonDictionaryArray {
            let item = BaseModel()
            item.id = dictionary["Id"] as? Int ?? 0
            
            item.name =  dictionary["Name"] as? String ?? "-"
            item.nameEn =   dictionary["NameEn"] as? String ?? "-"
                
            items.append(item)
        }
        
        return items
    }
}
