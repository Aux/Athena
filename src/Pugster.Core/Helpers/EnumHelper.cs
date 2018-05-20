namespace Pugster
{
    public static class EnumHelper
    {
        public static SkillRating GetSkillRating(int number)
        {
            if (number < 1) return SkillRating.Unranked;
            if (number < 1500) return SkillRating.Bronze;
            if (number < 2000) return SkillRating.Silver;
            if (number < 2500) return SkillRating.Gold;
            if (number < 3000) return SkillRating.Platinum;
            if (number < 3500) return SkillRating.Diamond;
            if (number < 4000) return SkillRating.Master;
            if (number <= 5000) return SkillRating.Grandmaster;
            return SkillRating.Unranked;
        }
    }
}
