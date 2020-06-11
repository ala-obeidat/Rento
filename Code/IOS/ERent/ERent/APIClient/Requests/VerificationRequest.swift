//
//  VerificationRequest.swift
//  ERent
//
//  Created by Anas Alhasani on 5/26/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct VerificationRequest {
    let code: Int
}

extension VerificationRequest: APIRequest {
    
    var resourceName: String {
        return "Account/Verification"
    }
    
    var parameters: JSONDictionary {
        return ["Data": ["Code": code]]
    }
    
    func makeModel(from json: JSONDictionary) -> Void {
        
    }
    
}
