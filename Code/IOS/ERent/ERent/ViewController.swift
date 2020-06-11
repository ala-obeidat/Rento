//
//  ViewController.swift
//  ERent
//
//  Created by Rexxer on 4/22/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import Koyomi

class ViewController: UIViewController {

    @IBOutlet weak var lbl: UILabel!
    //    @IBOutlet weak var calender: Koyomi!
    
    @IBOutlet fileprivate weak var calender: Koyomi! {
        didSet {
            calender.circularViewDiameter = 0.2
            calender.calendarDelegate = self
            calender.inset = UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 0)
            calender.weeks = ("Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat")
            calender.style = .standard
            calender.dayPosition = .center
            calender.selectionMode = .sequence(style: .semicircleEdge)
            calender.selectedStyleColor = UIColor(red: 203/255, green: 119/255, blue: 223/255, alpha: 1)
            calender
                .setDayFont(size: 14)
                .setWeekFont(size: 10)
        }
    }
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
//        self.lbl.text = "Rexxer".localizedLowercase
        self.lbl.text = NSLocalizedString("Rexxer", comment: "comment")
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }


}

// MARK: - KoyomiDelegate -

extension ViewController: KoyomiDelegate {
    func koyomi(_ koyomi: Koyomi, didSelect date: Date?, forItemAt indexPath: IndexPath) {
        print("You Selected: \(date)")
    }
    
    func koyomi(_ koyomi: Koyomi, currentDateString dateString: String) {
        //this property should be file private
        //        currentDateLabel.text = dateString
        
    }
    
    @objc(koyomi:shouldSelectDates:to:withPeriodLength:)
    func koyomi(_ koyomi: Koyomi, shouldSelectDates date: Date?, to toDate: Date?, withPeriodLength length: Int) -> Bool {
        //invalidPeriodLength should be file private
//        if length > invalidPeriodLength {
//            print("More than \(invalidPeriodLength) days are invalid period.")
//            return false
//        }
        return true
    }
}
