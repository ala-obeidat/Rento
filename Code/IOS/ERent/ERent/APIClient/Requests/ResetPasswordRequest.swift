//
//  ResetPasswordRequest.swift
//  ERent
//
//  Created by Rexxer on 5/28/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct ResetPasswordRequest {
    let code: Int
    let newPassword: String
    
}

extension ResetPasswordRequest : APIRequest {
    var parameters: JSONDictionary {
        return [
            "Data": [
                "Code": code,
                "NewPassword": newPassword,
                "Customer":1
            ]
        ]
    }
    
    var resourceName: String {
        return "Account/ResetPassword"
    }
    
    
    func makeModel(from json: JSONDictionary) -> Void { }
    
    
    
    
}
