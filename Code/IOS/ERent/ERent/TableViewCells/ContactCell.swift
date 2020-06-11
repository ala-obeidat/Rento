//
//  ContactCell.swift
//  ERent
//
//  Created by Zaid najjar on 6/29/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import MapKit

class ContactCell: UITableViewCell {

    @IBOutlet weak var number: UITextView!
    
    @IBOutlet weak var viewonmap: UIButton!
    var latitude : String? = ""
    var longitude : String? = ""
    var officeModel : OfficeLocation!
    
  
    var superVC : UIViewController? = nil
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }
    
    @IBAction func mapAction(_ sender: Any) {
     //   let dafq = officeModel.longitude
        openMapForPlace()
    }
    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)
        
        // Configure the view for the selected state
    }
    
    
    func populateWithCarModel(num : String) -> Void {
      
        number.text = num
        
    }
    
    func openMapForPlace() {
        
        let lat: CLLocationDegrees = CLLocationDegrees((latitude! as NSString).floatValue)
        let long: CLLocationDegrees = CLLocationDegrees((longitude! as NSString).floatValue)
        
        let regionDistance:CLLocationDistance = 10000
        let coordinates = CLLocationCoordinate2DMake(lat, long)
        let regionSpan = MKCoordinateRegionMakeWithDistance(coordinates, regionDistance, regionDistance)
        let options = [
            MKLaunchOptionsMapCenterKey: NSValue(mkCoordinate: regionSpan.center),
            MKLaunchOptionsMapSpanKey: NSValue(mkCoordinateSpan: regionSpan.span)
        ]
        let placemark = MKPlacemark(coordinate: coordinates, addressDictionary: nil)
        let mapItem = MKMapItem(placemark: placemark)
        mapItem.name = "E-Rent"
        mapItem.openInMaps(launchOptions: options)
    }
    
}
