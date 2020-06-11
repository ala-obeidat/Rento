//
//  LoginRequest.swift
//  ERent
//
//  Created by Anas Alhasani on 5/26/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct RefreshFcmRequest {
    let LoginToken: String
    let NotificationToken: String
}

extension RefreshFcmRequest: APIRequest {
    var parameters: JSONDictionary {
        return [
            "Data": [
                "LoginToken": LoginToken,
                "NotificationToken": NotificationToken
            ]
        ]
    }
    
    var resourceName: String {
        return "Account/RefreshToken"
    }
    
    
    func makeModel(from json: JSONDictionary) -> Void { }
    
    
    
    
}
