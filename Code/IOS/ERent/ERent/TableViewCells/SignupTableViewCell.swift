//
//  SignupTableViewCell.swift
//  ERent
//
//  Created by Rexxer on 5/10/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class SignupTableViewCell: UITableViewCell {

    
    @IBOutlet weak var sideButton: UIButton!
    @IBOutlet weak var bottomView: UIView!
    @IBOutlet weak var titleTextField: UITextField!
    
    @IBOutlet weak var containerView: UIView!
    
    var sideButtonImageName : String? = nil
    var placeHolderText : String? = nil
    
    
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

}
