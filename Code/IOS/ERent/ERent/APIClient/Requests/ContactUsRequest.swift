//
//  ContactUsRequest.swift
//  ERent
//
//  Created by Zaid najjar on 7/1/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

    struct ContactUsRequest {
        let subject: String
        let body: String
        let name: String
        let email: String
        let phone: String
}

extension ContactUsRequest: APIRequest {
    var parameters: JSONDictionary {
        return [
            "Data": [
                "Subject": subject,
                "Body": body,
                "Email": email,
                "Mobile": phone,
                "Name": name
            ]
        ]
    }
    
    var resourceName: String {
        return "Help/MessageContact"
    }
    
    
    func makeModel(from json: JSONDictionary) -> Void { }
    
    
    
    
}
