//
//  MessagesTableViewCell.swift
//  ERent
//
//  Created by Rexxer on 5/7/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit

class MessagesTableViewCell: UITableViewCell {
    @IBOutlet weak var dateLabel: UILabel!
    
    @IBOutlet weak var messageTitleLabel: UILabel!
    @IBOutlet weak var messageDetailsLabel: UILabel!
    var message : MessageModel!
    
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
 
    }

    
    
    func populateWithMessageModel(message : MessageModel) {
        self.message = message
        dateLabel.text = message.date
        messageTitleLabel.text = message.title
        messageDetailsLabel.text = message.details
        
    }
}
