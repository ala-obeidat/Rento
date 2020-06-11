//
//  CardViewCornered.swift
//  AKSwiftSlideMenu
//
//  Created by Zaid Khaled on 8/27/17.
//  Copyright © 2017 Kode. All rights reserved.
//

import UIKit

@IBDesignable
class CardViewCornered: UIView {
    
    @IBInspectable var cornerRadius: CGFloat = 11
    
    @IBInspectable var shadowOffsetWidth: Int = 0
    @IBInspectable var shadowOffsetHeight: Int = 3
    @IBInspectable var shadowColor: UIColor? = UIColor.black
    @IBInspectable var shadowOpacity: Float = 0.5
    
    override func layoutSubviews() {
        layer.cornerRadius = cornerRadius
        let shadowPath = UIBezierPath(roundedRect: bounds, cornerRadius: cornerRadius)
        
        layer.masksToBounds = false
        layer.shadowColor = shadowColor?.cgColor
        layer.shadowOffset = CGSize(width: shadowOffsetWidth, height: shadowOffsetHeight);
        layer.shadowOpacity = shadowOpacity
        layer.shadowPath = shadowPath.cgPath
    }
    
}
