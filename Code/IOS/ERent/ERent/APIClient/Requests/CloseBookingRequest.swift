//
//  CloseBookingRequest.swift
//  ERent
//
//  Created by Rexxer on 6/3/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import Foundation

struct CloseBookingRequest {
    let checkoutId: Int
    let star: Int
    let comment : String
    let flag: Int
}

extension CloseBookingRequest: APIRequest {
    var parameters: JSONDictionary {
        return [
            "Data": [
                "CheckoutId": checkoutId,
                "Star": star,
                "Comment": comment,
                "Flag": flag
            ]
        ]
    }
    
    var resourceName: String {
        return "Car/CloseBooking"
    }
    
    
    func makeModel(from json: JSONDictionary) -> Void { }
    
    
    
    
}
