//
//  CarRentingFilterTableViewCell.swift
//  ERent
//
//  Created by Rexxer on 4/30/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit


protocol CarRentingFilterTableViewCellDelegate: class {
    func didSelectCountryView()
    func didSelectCityView()
    func didSelectFromView()
    func didSelectToView()
    func didSelectSearch()
}


class CarRentingFilterTableViewCell: UITableViewCell {

    @IBOutlet weak var countryFilterView: UIView!
    @IBOutlet weak var countryLabel: UILabel!
    @IBOutlet weak var pickCountryButton: UIButton!
    
    
    @IBOutlet weak var cityFilterView: UIView!
    @IBOutlet weak var cityLabel: UILabel!
    @IBOutlet weak var pickCityButton: UIButton!
    
    
    @IBOutlet weak var fromFilterView: UIView!
    @IBOutlet weak var fromLabel: UILabel!
    @IBOutlet weak var pickFromButton: UIButton!
    
    @IBOutlet weak var toFilterView: UIView!
    @IBOutlet weak var toLabel: UILabel!
    @IBOutlet weak var pickToButton: UIButton!
    
    @IBOutlet weak var searchButton: UIButton!
    var delegate : CarRentingFilterTableViewCellDelegate? = nil
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
        
        self.searchButton.layer.cornerRadius = self.searchButton.frame.size.height/2
        self.searchButton.clipsToBounds = true
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
    
    @IBAction func countryButtonAction(_ sender: UIButton) {
        if delegate != nil {
            delegate?.didSelectCountryView()
        }
    }
    
    @IBAction func cityButtonAction(_ sender: UIButton) {
        if delegate != nil {
            delegate?.didSelectCityView()
        }
    }
    
    @IBAction func fromButtonAction(_ sender: UIButton) {
        if delegate != nil {
            delegate?.didSelectFromView()
        }
    }
    
    @IBAction func toButtonAction(_ sender: UIButton) {
        if delegate != nil {
            delegate?.didSelectToView()
        }
    }
    @IBAction func searchAction(_ sender: UIButton) {
        if delegate != nil {
            delegate?.didSelectSearch()
        }
    }
    
    
}
