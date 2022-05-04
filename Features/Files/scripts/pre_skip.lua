local pre_skip = {}

pre_skip.menu_main = {}
pre_skip.menu_cayo = {}
pre_skip.menu_casino = {}
pre_skip.menu_doomsday = {}
pre_skip.menu_apart = {}

function pre_skip.cayo(x)
    local v = {424340, 930000, 1030000, 1185000}
    mpIndex = stats.get_int("mpply_last_mp_char")
    mpx = "MP" .. mpIndex .. "_"
    stats.set_int(mpx .. "H4CNF_BS_GEN", 131071)
    stats.set_int(mpx .. "H4CNF_BS_ENTR", 63)
    stats.set_int(mpx .. "H4CNF_BS_ABIL", 63)
    stats.set_int(mpx .. "H4CNF_WEAPONS", 2)
    stats.set_int(mpx .. "H4CNF_WEP_DISRP", 3)
    stats.set_int(mpx .. "H4CNF_ARM_DISRP", 3)
    stats.set_int(mpx .. "H4CNF_HEL_DISRP", 3)
    stats.set_int(mpx .. "H4CNF_TARGET", 5)
    stats.set_int(mpx .. "H4CNF_BOLTCUT", 4424)
    stats.set_int(mpx .. "H4CNF_UNIFORM", 5256)
    stats.set_int(mpx .. "H4CNF_GRAPPEL", 5156)
    stats.set_int(mpx .. "H4CNF_TROJAN", 1)
    stats.set_int(mpx .. "H4CNF_APPROACH", -1)
    stats.set_int(mpx .. "H4LOOT_COKE_V", v[x])
    stats.set_int(mpx .. "H4LOOT_PAINT_V", v[x])
    stats.set_int(mpx .. "H4LOOT_CASH_I", 0)
    stats.set_int(mpx .. "H4LOOT_CASH_C", 0)
    stats.set_int(mpx .. "H4LOOT_WEED_I", 0)
    stats.set_int(mpx .. "H4LOOT_WEED_C", 0)
    stats.set_int(mpx .. "H4LOOT_COKE_I", -1)
    stats.set_int(mpx .. "H4LOOT_COKE_C", -1)
    stats.set_int(mpx .. "H4LOOT_GOLD_I", 0)
    stats.set_int(mpx .. "H4LOOT_GOLD_C", 0)
    stats.set_int(mpx .. "H4LOOT_PAINT", -1)
    stats.set_int(mpx .. "H4_PROGRESS", 126823)
    stats.set_int(mpx .. "H4LOOT_CASH_I_SCOPED", 0)
    stats.set_int(mpx .. "H4LOOT_CASH_C_SCOPED", 0)
    stats.set_int(mpx .. "H4LOOT_WEED_I_SCOPED", 0)
    stats.set_int(mpx .. "H4LOOT_WEED_C_SCOPED", 0)
    stats.set_int(mpx .. "H4LOOT_COKE_I_SCOPED", -1)
    stats.set_int(mpx .. "H4LOOT_COKE_C_SCOPED", -1)
    stats.set_int(mpx .. "H4LOOT_GOLD_I_SCOPED", 0)
    stats.set_int(mpx .. "H4LOOT_GOLD_C_SCOPED", 0)
    stats.set_int(mpx .. "H4LOOT_PAINT_SCOPED", -1)
    stats.set_int(mpx .. "H4_MISSIONS", 65535)
    stats.set_int(mpx .. "H4_PLAYTHROUGH_STATUS", 40000)
end

function pre_skip.show(parent)
    -- pre_skip.menu_main = parent:add_submenu("跳过前置")

    pre_skip.menu_cayo = pre_skip.menu_main:add_submenu("佩里科岛")
    pre_skip.menu_cayo:add_action("重置面板", function() 
        mpIndex = stats.get_int("mpply_last_mp_char")
        mpx = "MP" .. mpIndex .. "_"
        stats.set_int(mpx .. "H4_MISSIONS", 0)
        stats.set_int(mpx .. "H4_PROGRESS", 0)
        stats.set_int(mpx .. "H4_PLAYTHROUGH_STATUS", 0)
        stats.set_int(mpx .. "H4CNF_APPROACH", 0)
        stats.set_int(mpx .. "H4CNF_BS_ENTR", 0)
        stats.set_int(mpx .. "H4CNF_BS_GEN", 0)
    end)
    local cayo_approach = true
    local cayo_bs_entr = true
    pre_skip.menu_cayo:add_toggle("解锁所有入侵点", function() return cayo_approach end, function(value)
        cayo_approach = value
    end)
    pre_skip.menu_cayo:add_toggle("解锁所有豪宅入口", function() return cayo_bs_entr end, function(value)
        cayo_bs_entr = value
    end)
    local cayo_progress = 1
    local cayo_target = 4
    local cayo_weapons = 1
    local cayo_trojan = 4
    local cayo_bs_abil = true
    local cayo_bs_gen_grappel = true
    local cayo_bs_gen_uniform = true
    local cayo_bs_gen_boltcut = true
    local cayo_bs_gen_trojan = true
    local cayo_hujing = true
    local cayo_aer = true
    local cayo_meidusha = true
    local cayo_yinxing = true
    local cayo_xunluoting = true
    local cayo_changqi = true
    local cayo_disrp = true
    local cayo_loot_outside = 5
    local cayo_loot_inside = 5
    local cayo_loot_paint = true
    pre_skip.menu_cayo:add_array_item("任务难度", {"普通模式", "困难模式"}, function() return cayo_progress end, function(value)
        cayo_progress = value
    end)
    pre_skip.menu_cayo:add_array_item("主要目标", {"西西米托龙舌兰", "红宝石项链", "不记名债券", "粉钻", "玛徳拉索文件", "猎豹雕像"}, function() return cayo_target end, function(value)
        cayo_target = value
    end)
    pre_skip.menu_cayo:add_array_item("武器选择", {"侵略者", "阴谋者", "神枪手", "破坏者", "神射手"}, function() return cayo_weapons end, function(value)
        cayo_weapons = value
    end)
    pre_skip.menu_cayo:add_array_item("补给卡车位置", {"机场", "北码头", "主码头-东", "主码头-西", "豪宅门口"}, function() return cayo_trojan end, function(value)
        cayo_trojan = value
    end)
    pre_skip.menu_cayo:add_toggle("支援队伍", function() return cayo_bs_abil end, function(value) 
        cayo_bs_abil = value
    end)
    pre_skip.menu_cayo:add_toggle("兴趣点-抓钩", function() return cayo_bs_gen_grappel end, function(value) 
        cayo_bs_gen_grappel = value
    end)
    pre_skip.menu_cayo:add_toggle("兴趣点-保安服装", function() return cayo_bs_gen_uniform end, function(value) 
        cayo_bs_gen_uniform = value
    end)
    pre_skip.menu_cayo:add_toggle("兴趣点-螺栓切割器", function() return cayo_bs_gen_boltcut end, function(value) 
        cayo_bs_gen_boltcut = value
    end)
    pre_skip.menu_cayo:add_toggle("兴趣点-补给卡车", function() return cayo_bs_gen_trojan end, function(value) 
        cayo_bs_gen_trojan = value
    end)
    pre_skip.menu_cayo:add_toggle("兴趣点-补给卡车", function() return cayo_bs_gen_trojan end, function(value) 
        cayo_bs_gen_trojan = value
    end)
    pre_skip.menu_cayo:add_toggle("接近载具-虎鲸", function() return cayo_hujing end, function(value) 
        cayo_hujing = value
    end)
    pre_skip.menu_cayo:add_toggle("接近载具-阿尔科诺斯特", function() return cayo_aer end, function(value) 
        cayo_aer = value
    end)
    pre_skip.menu_cayo:add_toggle("接近载具-梅杜莎", function() return cayo_meidusha end, function(value) 
        cayo_meidusha = value
    end)
    pre_skip.menu_cayo:add_toggle("接近载具-隐形歼灭者", function() return cayo_yinxing end, function(value) 
        cayo_yinxing = value
    end)
    pre_skip.menu_cayo:add_toggle("接近载具-巡逻艇", function() return cayo_xunluoting end, function(value) 
        cayo_xunluoting = value
    end)
    pre_skip.menu_cayo:add_toggle("接近载具-长鳍", function() return cayo_changqi end, function(value) 
        cayo_changqi = value
    end)
    pre_skip.menu_cayo:add_toggle("干扰", function() return cayo_disrp end, function(value) 
        cayo_disrp = value
    end)
    pre_skip.menu_cayo:add_array_item("豪宅外次要目标", {"未侦察", "全现金", "全大麻", "全可卡因", "全黄金"}, function() return cayo_loot_outside end, function(value)
        cayo_loot_outside = value
    end)
    pre_skip.menu_cayo:add_array_item("豪宅内次要目标", {"未侦察", "全现金", "全大麻", "全可卡因", "全黄金"}, function() return cayo_loot_inside end, function(value)
        cayo_loot_inside = value
    end)
    pre_skip.menu_cayo:add_toggle("豪宅内名画", function() return cayo_loot_paint end, function(value) 
        cayo_loot_paint = value
    end)
    pre_skip.menu_cayo:add_action("一键跳过前置", function() 
        mpIndex = stats.get_int("mpply_last_mp_char")
        mpx = "MP" .. mpIndex .. "_"
        if cayo_approach == true then 
            stats.set_int(mpx .. "H4CNF_APPROACH", 255)
        end
        if cayo_bs_entr == true then 
            stats.set_int(mpx .. "H4CNF_BS_ENTR", 63)
        end
        local cayo_progress_v = {124271, 131055} -- 126823
        stats.set_int(mpx .. "H4_PROGRESS", cayo_progress_v[cayo_progress])
        stats.set_int(mpx .. "H4CNF_TARGET", cayo_target - 1)
        stats.set_int(mpx .. "H4CNF_WEAPONS", cayo_weapons)
        stats.set_int(mpx .. "H4CNF_TROJAN", cayo_trojan)
        if cayo_bs_abil == true then
            stats.set_int(mpx .. "H4CNF_BS_ABIL", 63)
        end
        local cayo_bs_gen_v = 81920
        if cayo_bs_gen_grappel == true then 
            cayo_bs_gen_v = cayo_bs_gen_v + 3840
        end
        if cayo_bs_gen_uniform == true then 
            cayo_bs_gen_v = cayo_bs_gen_v + 15
        end
        if cayo_bs_gen_boltcut == true then 
            cayo_bs_gen_v = cayo_bs_gen_v + 240
        end
        if cayo_bs_gen_trojan == true then
            cayo_bs_gen_v = cayo_bs_gen_v + 32768
        end
        stats.set_int(mpx .. "H4CNF_BS_GEN", cayo_bs_gen_v)
        local cayo_missions_v = 8065
        if cayo_hujing == true then 
            cayo_missions_v = cayo_missions_v + 2
        end
        if cayo_aer == true then 
            cayo_missions_v = cayo_missions_v + 4
        end
        if cayo_meidusha == true then 
            cayo_missions_v = cayo_missions_v + 8
        end
        if cayo_yinxing == true then 
            cayo_missions_v = cayo_missions_v + 16
        end
        if cayo_xunluoting == true then 
            cayo_missions_v = cayo_missions_v + 32
        end
        if cayo_changqi == true then 
            cayo_missions_v = cayo_missions_v + 64
        end 
        if cayo_disrp == true then 
            cayo_missions_v = cayo_missions_v + 57344
            stats.set_int(mpx .. "H4CNF_WEP_DISRP", 3)
            stats.set_int(mpx .. "H4CNF_ARM_DISRP", 3)
            stats.set_int(mpx .. "H4CNF_HEL_DISRP", 3)
        end
        stats.set_int(mpx .. "H4_MISSIONS", cayo_missions_v)
        if cayo_loot_outside == 1 then 
            stats.set_int(mpx .. "H4LOOT_CASH_I", 0)   
            stats.set_int(mpx .. "H4LOOT_WEED_I", 0)    
            stats.set_int(mpx .. "H4LOOT_COKE_I", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_I", 0)
            stats.set_int(mpx .. "H4LOOT_CASH_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_WEED_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_COKE_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_I_SCOPED", 0)
        end
        if cayo_loot_outside == 2 then 
            stats.set_int(mpx .. "H4LOOT_CASH_I", -1)   
            stats.set_int(mpx .. "H4LOOT_WEED_I", 0)    
            stats.set_int(mpx .. "H4LOOT_COKE_I", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_I", 0)
            stats.set_int(mpx .. "H4LOOT_CASH_I_SCOPED", -1)
            stats.set_int(mpx .. "H4LOOT_WEED_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_COKE_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_I_SCOPED", 0)
        end
        if cayo_loot_outside == 3 then 
            stats.set_int(mpx .. "H4LOOT_CASH_I", 0)   
            stats.set_int(mpx .. "H4LOOT_WEED_I", -1)    
            stats.set_int(mpx .. "H4LOOT_COKE_I", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_I", 0)
            stats.set_int(mpx .. "H4LOOT_CASH_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_WEED_I_SCOPED", -1)
            stats.set_int(mpx .. "H4LOOT_COKE_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_I_SCOPED", 0)
        end
        if cayo_loot_outside == 4 then 
            stats.set_int(mpx .. "H4LOOT_CASH_I", 0)   
            stats.set_int(mpx .. "H4LOOT_WEED_I", 0)    
            stats.set_int(mpx .. "H4LOOT_COKE_I", -1)
            stats.set_int(mpx .. "H4LOOT_GOLD_I", 0)
            stats.set_int(mpx .. "H4LOOT_CASH_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_WEED_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_COKE_I_SCOPED", -1)
            stats.set_int(mpx .. "H4LOOT_GOLD_I_SCOPED", 0)
        end
        if cayo_loot_outside == 5 then 
            stats.set_int(mpx .. "H4LOOT_CASH_I", 0)   
            stats.set_int(mpx .. "H4LOOT_WEED_I", 0)    
            stats.set_int(mpx .. "H4LOOT_COKE_I", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_I", -1)
            stats.set_int(mpx .. "H4LOOT_CASH_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_WEED_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_COKE_I_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_I_SCOPED", -1)
        end
        if cayo_loot_inside == 1 then 
            stats.set_int(mpx .. "H4LOOT_CASH_C", 0)         
            stats.set_int(mpx .. "H4LOOT_WEED_C", 0)         
            stats.set_int(mpx .. "H4LOOT_COKE_C", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_C", 0)
            stats.set_int(mpx .. "H4LOOT_CASH_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_WEED_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_COKE_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_C_SCOPED", 0)
        end
        if cayo_loot_inside == 2 then 
            stats.set_int(mpx .. "H4LOOT_CASH_C", -1)         
            stats.set_int(mpx .. "H4LOOT_WEED_C", 0)         
            stats.set_int(mpx .. "H4LOOT_COKE_C", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_C", 0)
            stats.set_int(mpx .. "H4LOOT_CASH_C_SCOPED", -1)
            stats.set_int(mpx .. "H4LOOT_WEED_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_COKE_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_C_SCOPED", 0)
        end 
        if cayo_loot_inside == 3 then 
            stats.set_int(mpx .. "H4LOOT_CASH_C", 0)         
            stats.set_int(mpx .. "H4LOOT_WEED_C", -1)         
            stats.set_int(mpx .. "H4LOOT_COKE_C", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_C", 0)
            stats.set_int(mpx .. "H4LOOT_CASH_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_WEED_C_SCOPED", -1)
            stats.set_int(mpx .. "H4LOOT_COKE_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_C_SCOPED", 0)
        end
        if cayo_loot_inside == 4 then 
            stats.set_int(mpx .. "H4LOOT_CASH_C", 0)         
            stats.set_int(mpx .. "H4LOOT_WEED_C", 0)         
            stats.set_int(mpx .. "H4LOOT_COKE_C", -1)
            stats.set_int(mpx .. "H4LOOT_GOLD_C", 0)
            stats.set_int(mpx .. "H4LOOT_CASH_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_WEED_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_COKE_C_SCOPED", -1)
            stats.set_int(mpx .. "H4LOOT_GOLD_C_SCOPED", 0)
        end
        if cayo_loot_inside == 5 then 
            stats.set_int(mpx .. "H4LOOT_CASH_C", 0)         
            stats.set_int(mpx .. "H4LOOT_WEED_C", 0)         
            stats.set_int(mpx .. "H4LOOT_COKE_C", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_C", -1)
            stats.set_int(mpx .. "H4LOOT_CASH_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_WEED_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_COKE_C_SCOPED", 0)
            stats.set_int(mpx .. "H4LOOT_GOLD_C_SCOPED", -1)
        end
        if cayo_loot_paint == true then 
            stats.set_int(mpx .. "H4LOOT_PAINT", -1)
            stats.set_int(mpx .. "H4LOOT_PAINT_SCOPED", -1)
        else
            stats.set_int(mpx .. "H4LOOT_PAINT", 0)
            stats.set_int(mpx .. "H4LOOT_PAINT_SCOPED", 0)
        end
        stats.set_int(mpx .. "H4_PLAYTHROUGH_STATUS", 10)     
    end)
    pre_skip.menu_cayo:add_action("默认跳(给1人)-猎豹+修改次要目标价值", function() 
        pre_skip.cayo(1)
    end)
    pre_skip.menu_cayo:add_action("默认跳(给2人)-猎豹+修改次要目标价值", function() 
        pre_skip.cayo(2)
    end)
    pre_skip.menu_cayo:add_action("默认跳(给3人)-猎豹+修改次要目标价值", function() 
        pre_skip.cayo(3)
    end)
    pre_skip.menu_cayo:add_action("默认跳(给4人)-猎豹+修改次要目标价值", function() 
        pre_skip.cayo(4)
    end)

    pre_skip.menu_casino = pre_skip.menu_main:add_submenu("名钻赌场")
    pre_skip.menu_casino:add_action("解除冷却时间", function()
        mpIndex = stats.get_int("mpply_last_mp_char")
        mpx = "MP" .. mpIndex .. "_"
        stats.set_int(mpx .. "H3_COMPLETEDPOSIX", -1)
    end)
    pre_skip.menu_casino:add_action("重置面板", function()
        mpIndex = stats.get_int("mpply_last_mp_char")
        mpx = "MP" .. mpIndex .. "_"
        stats.set_int(mpx .. "H3OPT_BITSET1", 0)
        stats.set_int(mpx .. "H3OPT_BITSET0", 0)
        stats.set_int(mpx .. "H3OPT_POI", 0)
        stats.set_int(mpx .. "H3OPT_ACCESSPOINTS", 0)
    end)
    local casino_accesspoints = true
    local casino_poi = true
    pre_skip.menu_casino:add_toggle("解锁所有出入口", function() return casino_accesspoints end, function(value) 
        casino_accesspoints = value
    end)
    pre_skip.menu_casino:add_toggle("解锁所有兴趣点", function() return casino_poi end, function(value) 
        casino_poi = value
    end)
    local casino_target = 3
    local casino_approach = 2
    local casino_hard_approach = 2
    pre_skip.menu_casino:add_array_item("抢劫财物", {"现金", "黄金", "名画", "钻石"}, function() return casino_target end, function(value)
        casino_target = value
    end)
    pre_skip.menu_casino:add_array_item("进入方式", {"隐匿潜踪", "兵不厌诈", "气势汹汹"}, function() return casino_approach end, function(value)
        casino_approach = value
    end)
    pre_skip.menu_casino:add_array_item("困难模式", {"隐匿潜踪", "兵不厌诈", "气势汹汹"}, function() return casino_hard_approach end, function(value)
        casino_hard_approach = value
    end)
    local casino_crewweap = 1
    local casino_crewdriver = 1
    local casino_crewhacker = 4
    local casino_vehs = 1
    local casino_weaps = 1
    local casino_disruptship = true
    local casino_masks = 1
    local casino_keylevels = 2
    pre_skip.menu_casino:add_array_item("枪手选择", {"5%", "9%", "7%", "10%", "8%", "0%"}, function() return casino_crewweap end, function(value)
        casino_crewweap = value
    end)
    pre_skip.menu_casino:add_array_item("车手选择", {"5%", "7%", "9%", "6%", "10%", "0%"}, function() return casino_crewdriver end, function(value)
        casino_crewdriver = value
    end)
    pre_skip.menu_casino:add_array_item("黑客选择", {"3%", "7%", "5%", "10%", "9%", "0%"}, function() return casino_crewhacker end, function(value)
        casino_crewhacker = value
    end)
    pre_skip.menu_casino:add_array_item("载具类型", {"类型一", "类型二", "类型三", "类型四"}, function() return casino_vehs end, function(value)
        casino_vehs = value
    end)
    pre_skip.menu_casino:add_array_item("武器类型", {"类型一", "类型二"}, function() return casino_weaps end, function(value)
        casino_weaps = value
    end)
    pre_skip.menu_casino:add_toggle("杜根货物", function() return casino_disruptship end, function(value)
        casino_disruptship = value
    end)
    pre_skip.menu_casino:add_array_item("面具类型", {"无", "几何灰白猫", "小猪", "绿色半鬼面", "大笑", "蓝色华丽纹饰骷髅头", "霓虹小丑", "黑色公牛", "日出针织头套", "白色鬼面", "潮大支席桑达·诺芙滑稽"}, function() return casino_masks end, function(value)
        casino_masks = value
    end)
    pre_skip.menu_casino:add_array_item("钥匙等级", {"一级", "二级"}, function() return casino_keylevels end, function(value)
        casino_keylevels = value
    end)
    pre_skip.menu_casino:add_action("一键跳过前置", function()
        mpIndex = stats.get_int("mpply_last_mp_char")
        mpx = "MP" .. mpIndex .. "_"
        if casino_accesspoints == true then 
            stats.set_int(mpx .. "H3OPT_ACCESSPOINTS", 2047)
        end
        if casino_poi == true then 
            stats.set_int(mpx .. "H3OPT_POI", 2047)
        end
        stats.set_int(mpx .. "H3OPT_TARGET", casino_target - 1)
        stats.set_int(mpx .. "H3OPT_APPROACH", casino_approach)
        stats.set_int(mpx .. "H3_HARD_APPROACH", casino_hard_approach)
        stats.set_int(mpx .. "H3OPT_BITSET1", -1)
        stats.set_int(mpx .. "H3OPT_CREWWEAP", casino_crewweap)
        stats.set_int(mpx .. "H3OPT_CREWDRIVER", casino_crewdriver)
        stats.set_int(mpx .. "H3OPT_CREWHACKER", casino_crewhacker)
        stats.set_int(mpx .. "H3OPT_VEHS", casino_vehs - 1)
        stats.set_int(mpx .. "H3OPT_WEAPS", casino_weaps - 1)
        if casino_disruptship == true then
            stats.set_int(mpx .. "H3OPT_DISRUPTSHIP", 3)
        end
        stats.set_int(mpx .. "H3OPT_MASKS", casino_masks)
        stats.set_int(mpx .. "H3OPT_KEYLEVELS", casino_keylevels)
        stats.set_int(mpx .. "H3OPT_BITSET0", -194)
    end)

    -- pre_skip.menu_casino:add_submenu("--------以下内容为提示--------")
    -- pre_skip.menu_casino:add_submenu("如果你的出入口或兴趣点没有解锁")
    -- pre_skip.menu_casino:add_submenu("完全，在选择出入口的时候可能会卡退")

    pre_skip.menu_doomsday = pre_skip.menu_main:add_submenu("末日抢劫")
    pre_skip.menu_doomsday:add_action("解锁末日抢劫", function()
        mpIndex = stats.get_int("mpply_last_mp_char")
        mpx = "MP" .. mpIndex .. "_"
        stats.set_int(mpx .. "GANGOPS_HEIST_STATUS", 9999)
    end)
    pre_skip.menu_doomsday:add_action("开启数据泄露(末日一)", function()
        mpIndex = stats.get_int("mpply_last_mp_char")
        mpx = "MP" .. mpIndex .. "_"
        stats.set_int(mpx .. "GANGOPS_FLOW_MISSION_PROG", 503)
        stats.set_int(mpx .. "GANGOPS_HEIST_STATUS", 229383)
        stats.set_int(mpx .. "GANGOPS_FLOW_NOTIFICATIONS", 1557)
    end)
    pre_skip.menu_doomsday:add_action("解锁波格丹危机(末日二)", function()
        mpIndex = stats.get_int("mpply_last_mp_char")
        mpx = "MP" .. mpIndex .. "_"
        stats.set_int(mpx .. "GANGOPS_FLOW_MISSION_PROG", 240)
        stats.set_int(mpx .. "GANGOPS_HEIST_STATUS", 229378)
        stats.set_int(mpx .. "GANGOPS_FLOW_NOTIFICATIONS", 1557)
    end)
    pre_skip.menu_doomsday:add_action("解锁末日将至(末日三)", function()
        mpIndex = stats.get_int("mpply_last_mp_char")
        mpx = "MP" .. mpIndex .. "_"
        stats.set_int(mpx .. "GANGOPS_FLOW_MISSION_PROG", 16368)
        stats.set_int(mpx .. "GANGOPS_HEIST_STATUS", 229380)
        stats.set_int(mpx .. "GANGOPS_FLOW_NOTIFICATIONS", 1557)
    end)

    pre_skip.menu_apart = pre_skip.menu_main:add_submenu("公寓抢劫")
    pre_skip.menu_apart:add_action("跳过准备任务", function() 
        mpIndex = stats.get_int("mpply_last_mp_char")
        mpx = "MP" .. mpIndex .. "_"
        stats.set_int(mpx .. "HEIST_PLANNING_STAGE", -1)
    end)
    
end

pre_skip.menu_main = menu.add_submenu("跳过前置 By Aure")

pre_skip.show(pre_skip.menu_main)

-- return pre_skip