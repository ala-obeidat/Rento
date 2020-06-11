//
//  ForgotPasswordRequest.swift
//  ERent
//
//  Created by Anas Alhasani on 5/26/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct ForgotPasswordRequest {
    let username: String
}

extension ForgotPasswordRequest: APIRequest {
    
    var resourceName: String {
        return "Account/ForgetPassword"
    }
    
    var parameters: JSONDictionary {
        return [ "Data": username]
    }

    func makeModel(from json: JSONDictionary) -> Void { }
}
