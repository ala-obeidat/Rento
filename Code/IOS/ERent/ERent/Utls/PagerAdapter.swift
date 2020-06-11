//
//  PagerAdapter.swift
//  ERent
//
//  Created by Rexxer on 5/15/18.
//  Copyright © 2018 Rexxer. All rights reserved.
//

import UIKit
import XLPagerTabStrip
class PagerAdapter: ButtonBarPagerTabStripViewController {



    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    

    let purpleInspireColor = UIColor.AppTheme()
    override func viewDidLoad() {
        
        // change selected bar color
        settings.style.buttonBarBackgroundColor = .white
        settings.style.buttonBarItemBackgroundColor = .white
        settings.style.selectedBarBackgroundColor = purpleInspireColor
        settings.style.buttonBarItemFont = .boldSystemFont(ofSize: 14)
        settings.style.selectedBarHeight = 2.0
        settings.style.buttonBarMinimumLineSpacing = 0
        settings.style.buttonBarItemTitleColor = .black
        settings.style.buttonBarItemsShouldFillAvailiableWidth = true
        settings.style.buttonBarLeftContentInset = 0
        settings.style.buttonBarRightContentInset = 0
        changeCurrentIndexProgressive = { [weak self] (oldCell: ButtonBarViewCell?, newCell: ButtonBarViewCell?, progressPercentage: CGFloat, changeCurrentIndex: Bool, animated: Bool) -> Void in
            guard changeCurrentIndex == true else { return }
            oldCell?.label.textColor = .black
            newCell?.label.textColor = self?.purpleInspireColor
        }
        
//        view.transform  = CGAffineTransform(scaleX: -1, y: 1);
        super.viewDidLoad()
    }

    override func viewControllers(for pagerTabStripController: PagerTabStripViewController) -> [UIViewController] {
         let contactTab = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "ContactUsTab")
        let favoriteViewController = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "FavoriteViewController")
        let historyViewController = UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: "HistoryViewController")
        
        
//        if L102Language.currentAppleLanguage() == "en" {
//        return [favoriteViewController, historyViewController]
//
//        } else {
//            return [historyViewController ,favoriteViewController ]
//        }
        return [contactTab,favoriteViewController, historyViewController]
    }
}
