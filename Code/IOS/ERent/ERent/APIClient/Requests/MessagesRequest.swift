//
//  MessagesRequest.swift
//  ERent
//
//  Created by Rexxer on 5/27/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct MessagesRequest {
   
}

extension MessagesRequest: APIRequest {
    var parameters: JSONDictionary {
        return [:]
    }
    
    var resourceName: String {
        return "Message/List"
    }
    
    
    func makeModel(from json: JSONDictionary) -> [MessageModel] {
        
        
        var messages: [MessageModel] = []
        
        //FIXME: - Double check CarsArray Key
        let jsonDictionaryArray = json["Data"] as? [JSONDictionary] ?? []
        
        for dictionary in jsonDictionaryArray {
            let message = MessageModel()
        
            message.identifier = dictionary["Id"] as? Int ?? 0
            message.details = dictionary["Body"] as? String ?? "-"
            
            let str = dictionary["CreateDate"] as? String ?? "-"
            let substring = str[..<10]
            if str.count >= 10 {
                let string = String(substring)
                message.date = string
                
            }else{
                message.date = str
                
            }
            messages.append(message)
        }
        
        return messages
    }
        
        
}

