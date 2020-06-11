//
//  LoginRequest.swift
//  ERent
//
//  Created by Anas Alhasani on 5/26/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct LoginRequest {
    let username: String
    let password: String
}

extension LoginRequest: APIRequest {
    var parameters: JSONDictionary {
        return [
            "Data": [
                "Username": username,
                "Password": password,
                "Customer":1
            ]
        ]
    }
    
    var resourceName: String {
        return "Account/Login"
    }
    
    
    func makeModel(from json: JSONDictionary) -> Void { }
    

    
    
}
