//
//  carModel.swift
//  ERent
//
//  Created by Rexxer on 4/29/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class AccountModel: NSObject ,NSCoding {

    var LoginToken : String? = nil
    var NotificationToken : String? = nil
   
    var loginToken = ""
    var notificationToken = ""
 

    required convenience init(coder aDecoder: NSCoder) {
        
        self.init()
       
        loginToken = aDecoder.decodeObject(forKey: "LoginToken") as! String
        notificationToken = aDecoder.decodeObject(forKey: "NotificationToken") as! String
       
    }
    
    
    func encode(with aCoder: NSCoder) {
    
        aCoder.encode(LoginToken,forKey:"LoginToken")
        aCoder.encode(NotificationToken,forKey:"NotificationToken")
        
    }
    
}
